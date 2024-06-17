using Astrum.Calendar.Application.Services;
using Astrum.Calendar.Services;
using Astrum.Calendar.ViewModels;
namespace Astrum.Calendar.GraphQL;
public class QueryCalendar
{
    [UseSorting]
    [UseFiltering]
    public async Task<List<CalendarForm>> GetCalendarList([Service] ICalendarEventService calendarListService)
    {
        return await calendarListService.GetCalendarEventsAsync(DateTime.MinValue, DateTime.MaxValue);
    }
}
