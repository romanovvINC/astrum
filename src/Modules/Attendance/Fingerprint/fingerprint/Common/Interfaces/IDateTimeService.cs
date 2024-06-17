namespace FuckWeb.Common.Interfaces;

public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime ConvertToUserTime(DateTime dateTime);
    DateTime CurrentUserTime();
}