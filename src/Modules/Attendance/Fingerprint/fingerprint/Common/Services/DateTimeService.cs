using FuckWeb.Common.Interfaces;

namespace FuckWeb.Common.Services;

public class DateTimeService:IDateTimeService
{
    private readonly ITimeZoneService _timeZoneService;

    public DateTimeService(ITimeZoneService timeZoneService)
    {
        _timeZoneService = timeZoneService;
    }

    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime CurrentUserTime() => ConvertToUserTime(DateTime.UtcNow);

    public DateTime ConvertToUserTime(DateTime dateTime)
    {
        var userTimeZone = _timeZoneService.GetUserTimeZone();
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, userTimeZone);
        }
        else
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc, userTimeZone);
        }
    }
}