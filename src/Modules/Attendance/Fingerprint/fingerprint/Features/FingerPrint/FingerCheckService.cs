using FuckWeb.Data;
using FuckWeb.Data.Entities;
using FuckWeb.Features.Notifier;

namespace FuckWeb.Features.FingerPrint;

public class FingerCheckService
{
    private FDbContext _ctx;

    public FingerCheckService(FDbContext ctx)
    {
        _ctx = ctx;
    }

    public string Check(string fingerId)
    {
        var user = _ctx.Users.FirstOrDefault(x => x.FingerId == fingerId);
        if (user == null)
            throw new Exception("Finger not found!");

        var fingerCheckSession = new FingerCheck()
        {
            Date = DateTime.UtcNow,
            User = user
        };

        string resultMessage;
        var lastCheck = _ctx.FingerChecks.Where(x => x.User == user)
            .Where(x=>x.Date> DateTime.Today)
            .OrderByDescending(x => x.Date).FirstOrDefault();
        if (lastCheck is null)
        {
            _ctx.FingerChecks.Add(fingerCheckSession);
            resultMessage = $"Privet, {user.Name}";
        }
        else if ((DateTime.UtcNow - lastCheck.Date).Minutes < 1)
        {
            resultMessage = $"  Allready checked, {user.Name}";
        }
        else
        {
            var checkCount = _ctx.FingerChecks.Count(x => x.User == user && x.Date > DateTime.Today);
            var phrase = checkCount == 1 ? "Poka" : checkCount % 2 == 0 ? "    Snova privet  " : "Poka" ;
            _ctx.FingerChecks.Add(fingerCheckSession);
            resultMessage = $"{phrase}, {user.Name}";
        }
        _ctx.SaveChanges();
        //TgNotifier.Notify(resultMessage);
        return resultMessage;
    }
}