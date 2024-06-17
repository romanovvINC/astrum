using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Astrum.Calendar.Domain.Aggregates;
using Astrum.Calendar.Domain.Repositories;
using Astrum.Calendar.Domain.Specifications;
using Astrum.Calendar.Services;
using Astrum.Calendar.ViewModels;
using Astrum.SharedLib.Common.Results;
using AutoMapper;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.Configuration;
using Event = Google.Apis.Calendar.v3.Data.Event;

namespace Astrum.Calendar.Application.Services
{
    public class CalendarEventService : ICalendarEventService
    {
        private readonly ICalendarRepository<Domain.Aggregates.Calendar> _calendarRepository;
        private readonly ICalendarRepository<Domain.Aggregates.Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly IAccessorService _accessorService;
        private readonly IConfiguration _config;

        public CalendarEventService(ICalendarRepository<Domain.Aggregates.Calendar> calendarRepository, IMapper mapper, 
            ICalendarRepository<Domain.Aggregates.Event> eventRepository, IAccessorService accessorService, IConfiguration configuration, IConfiguration config)
        {
            _calendarRepository = calendarRepository;
            _mapper = mapper;
            _eventRepository = eventRepository;
            _accessorService = accessorService;
            _config = config;
        }

        public async Task<Result<List<CalendarForm>>> GetCalendarEventsAsync(DateTime start, DateTime end)
        {
            var spec = new GetCalendarEventsSpec(start, end);
            var calendars = await _calendarRepository.ListAsync(spec);
            var result = _mapper.Map<List<CalendarForm>>(calendars);
            return Result.Success(result);
        }

        public async Task<Result<CalendarForm>> CreateCalendar(string summary)
        {
            var spec = new GetCalendarBySummarySpec(summary);
            var exist = await _calendarRepository.FirstOrDefaultAsync(spec);
            if (exist != null)
            {
                return Result<CalendarForm>.Error("Календарь с таким именем уже существует");
            }

            var random = new Random();
            var color = $"#{random.Next(0x1000000):X6}"; // = "#A197B9"
            var calendar = new Domain.Aggregates.Calendar()
            {
                Summary = summary,
                BackgroundColor = color
            };
            var newCalendar = await _calendarRepository.AddAsync(calendar);
            try
            {
                await _calendarRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при создании календаря.");
            }
            var result = _mapper.Map<CalendarForm>(newCalendar);
            return Result.Success(result);
        }

        public async Task<Result<CalendarForm>> UpdateCalendar(Guid id, CalendarForm form)
        {
            var spec = new GetCalendarByIdSpec(id);
            var calendar = await _calendarRepository.FirstOrDefaultAsync(spec);
            if (calendar == null) 
                return Result.NotFound("Календарь не найден");

            _mapper.Map(form, calendar);
            try
            {
                await _calendarRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при обновлении календаря.");
            }

            var updated = _mapper.Map<CalendarForm>(calendar);
            return Result.Success(updated);
        }

        public async Task<Result<CalendarForm>> DeleteCalendar(Guid id)
        {
            var spec = new GetCalendarByIdSpec(id);
            var calendar = await _calendarRepository.FirstOrDefaultAsync(spec);
            if (calendar == null)
                return Result.NotFound("Календарь не найден");
            await _calendarRepository.DeleteAsync(calendar);
            try
            {
                await _calendarRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при удалении календаря.");
            }
            return Result.Success();
        }

        public async Task<Result<EventForm>> CreateEvent(EventForm form)
        {
            var @event = _mapper.Map<Domain.Aggregates.Event>(form);
            var newEvent = await _eventRepository.AddAsync(@event);
            try
            {
                await _eventRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при создании событие.");
            }
            var result = _mapper.Map<EventForm>(newEvent);
            return Result.Success(result);
        }

        public async Task<Result<EventForm>> UpdateEvent(Guid id, EventForm form)
        {
            var spec = new GetEventByIdSpec(id);
            var @event = await _eventRepository.FirstOrDefaultAsync(spec);
            if (@event == null)
                return Result.NotFound("Событие не найдено");

            _mapper.Map(form, @event);
            try
            {
                await _calendarRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при обновлении события.");
            }

            var updated = _mapper.Map<EventForm>(@event);
            return Result.Success(updated);
        }

        public async Task<Result<EventForm>> DeleteEvent(Guid id)
        {
            var spec = new GetEventByIdSpec(id);
            var @event = await _eventRepository.FirstOrDefaultAsync(spec);
            if (@event == null)
                return Result.NotFound("Событие не найдено");
            await _eventRepository.DeleteAsync(@event);
            try
            {
                await _calendarRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при удалении события.");
            }

            var form = _mapper.Map<EventForm>(@event);
            return Result.Success(form);
        }

        public async Task<Result> SyncCalendars()
        {
            var service = await GetAccess();
            if (service.Failed)
            {
                return Result.Error(service.MessageWithErrors);
            }

            var calendarList = new CalendarList();
            try
            {
                var calendarIds = _config.GetSection("Calendar:Available-Calendars").Get<string[]>();
                foreach (var id in calendarIds)
                {
                    var newCalendar = new CalendarListEntry()
                    {
                        Id = id
                    };
                    await service.Data.CalendarList.Insert(newCalendar).ExecuteAsync();
                }
                
                calendarList = await service.Data.CalendarList.List().ExecuteAsync();
            }
            catch (Exception e)
            {
                return Result.Error($"Не удалось получить календари. Ошибка: {e.Message}");
            }

            var spec = new GetCalendarByGoogleIdSpec(calendarList.Items.Select(x => x.Id));
            var calendars = await _calendarRepository.ListAsync(spec);
            foreach (var entry in calendarList.Items)
            {
                var calendarId = await GetCalendarId(calendars, entry);
                var eventResult = await SyncEvents(service, entry, calendarId);
                if (eventResult.Failed)
                {
                    return Result.Error(eventResult.MessageWithErrors);
                }
            }

            try
            {
                await _calendarRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при сохранении.");
            }

            return Result.Success();
        }

        private async Task<Guid> GetCalendarId(List<Domain.Aggregates.Calendar> calendars, CalendarListEntry calendar)
        {
            Guid calendarId;
            if (!calendars.Select(x => x.GoogleId).Contains(calendar.Id))
            {
                var calendarDb = _mapper.Map<Domain.Aggregates.Calendar>(calendar);
                var newCalendar = await _calendarRepository.AddAsync(calendarDb);
                calendarId = newCalendar.Id;
            }
            else
            {
                calendarId = calendars.FirstOrDefault(x => x.GoogleId == calendar.Id).Id;
            }

            return calendarId;
        }

        private async Task<Result> SyncEvents(CalendarService service, CalendarListEntry calendar, 
            Guid calendarId)
        {
            Events googleEvents;
            try
            {
                googleEvents = await service.Events.List(calendar.Id).ExecuteAsync();
            }
            catch (Exception e)
            {
                return Result.Error($"Не удалось получить события календаря. Ошибка: {e.Message}");
            }

            var spec = new GetEventByGoogleIdSpec(googleEvents.Items.Select(x => x.Id));
            var events = await _eventRepository.ListAsync(spec);
            var calendarIds = events.Select(x => x.GoogleId);
            var items = googleEvents.Items.Where(x => !calendarIds.Contains(x.Id));
            var eventsDb = _mapper.Map<List<Domain.Aggregates.Event>>(items);
            eventsDb.ForEach(x => x.CalendarId = calendarId);
            await _eventRepository.AddRangeAsync(eventsDb);

            return Result.Success();
        }

        private async Task<Result<CalendarService>> GetAccess()
        {
            CalendarService result;
            try
            {
                result = await _accessorService.GetAccess();
            }
            catch (Exception ex)
            {
                return Result.Error($"Не удалось получить доступ к календарю. {ex}");
            }

            return Result.Success(result);
        }
    }
}
