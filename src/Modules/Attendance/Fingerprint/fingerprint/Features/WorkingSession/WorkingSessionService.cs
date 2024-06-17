using FuckWeb.Data;
using FuckWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuckWeb.Features.WorkingSession;

public class WorkingSessionService
{
    private FDbContext _ctx;

    public WorkingSessionService(FDbContext ctx)
    {
        _ctx = ctx;
    }

    public List<WorkingSession> GetWeeklyUserSessions(long userId)
    {
        return GetUserSessions(userId, DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
    }

    public List<WorkingSession> GetUserSessions(long userId, DateTime fromDate, DateTime toDate)
    {
        var user = _ctx.Users.Find(userId);
        var userChecks = _ctx.FingerChecks
            .Where(x => x.UserId == userId)
            .Where(x => x.Date >= fromDate)
            .Where(x => x.Date <= toDate)
            .ToList();

        var groupedCheckByDay = userChecks.GroupBy(x => DateOnly.FromDateTime(x.Date));
        return groupedCheckByDay.Select(x => new WorkingSession(x.ToList(), user, x.Key)).ToList();
    }

    public List<WorkingSession> GetSessions(DateTime fromDate, DateTime toDate)
    {
        var usersChecks = _ctx.FingerChecks
            .Include(x => x.User)
            .Where(x => x.Date >= fromDate.ToUniversalTime())
            .Where(x => x.Date <= toDate.ToUniversalTime())
            .ToList()
            .GroupBy(x => x.User);

        var sessions = new List<WorkingSession>();
        foreach (var userCheck in usersChecks)
        {
            var groupedCheckByDay = userCheck.GroupBy(x => DateOnly.FromDateTime(x.Date));
            sessions.AddRange(groupedCheckByDay.Select(x => new WorkingSession(x.ToList(), userCheck.Key, x.Key)).ToList());
        }
        return sessions;
    }

    public List<WorkingSession> GetTodaySessions() => GetSessions(DateTime.UtcNow.Date, DateTime.UtcNow);

}