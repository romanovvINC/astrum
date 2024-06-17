using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Calendar.ViewModels;
using Astrum.SharedLib.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Astrum.Calendar.Application.Services
{
    public interface ICalendarEventService
    {
        Task<Result<List<CalendarForm>>> GetCalendarEventsAsync(DateTime start, DateTime end);
        Task<Result> SyncCalendars();
        Task<Result<CalendarForm>> CreateCalendar(string summary);
        Task<Result<CalendarForm>> UpdateCalendar(Guid id, CalendarForm form);
        Task<Result<CalendarForm>> DeleteCalendar(Guid id);
        Task<Result<EventForm>> CreateEvent(EventForm form);
        Task<Result<EventForm>> UpdateEvent(Guid id, EventForm form);
        Task<Result<EventForm>> DeleteEvent(Guid id);
    }
}
