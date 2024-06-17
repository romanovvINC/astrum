using FuckWeb.Common.Interfaces;

namespace FuckWeb.Common.Services;

public class TimeZoneService : ITimeZoneService
{
    private const string TimeZoneCookieName = "userTimeZone";
    private TimeZoneInfo _userTimeZone = TimeZoneInfo.Utc;

    public TimeZoneService(IHttpContextAccessor httpContextAccessor)
    {
        var userTimeZone = httpContextAccessor.HttpContext?.Request.Cookies[TimeZoneCookieName];
        if (!string.IsNullOrEmpty(userTimeZone))
        {
            _userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZone);
        }
    }
    
    public TimeZoneInfo GetUserTimeZone() => _userTimeZone;
}