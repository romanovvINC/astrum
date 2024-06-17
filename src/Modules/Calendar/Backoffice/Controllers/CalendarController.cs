using Astrum.Calendar.Application.Services;
using Astrum.Calendar.Services;
using Astrum.Calendar.ViewModels;
using Astrum.Infrastructure.Shared;
using Astrum.Infrastructure.Shared.Result.AspNetCore;
using Astrum.Logging.Services;
using Astrum.SharedLib.Common.Results;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Astrum.Calendar.Controllers;

//[Authorize(Roles = "admin")]
[Area("Calendar")]
[Route("[controller]")]
public class CalendarController : ApiBaseController
{
    private readonly ICalendarEventService _calendarService;
    private readonly ILogHttpService _logger;

    public CalendarController(ICalendarEventService calendarService, ILogHttpService logger)
    {
        _calendarService = calendarService;
        _logger = logger;
    }

    /// <summary>
    /// Синхронизация календарей и событий из Google
    /// </summary>
    /// <param name="request">Начало и конец недели для получения</param>
    /// <returns></returns>
    [HttpPost("sync")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<CalendarForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result> Sync()
    {
        var result = await _calendarService.SyncCalendars();
        _logger.Log(null, result, HttpContext, "Синхронизация календарей и событий из Google", Logging.Entities.TypeRequest.POST, Logging.Entities.ModuleAstrum.Calendar);
        return result;
    }

    /// <summary>
    /// Получение календарей и событий
    /// </summary>
    /// <param name="request">Начало и конец недели для получения</param>
    /// <returns></returns>
    [HttpGet("{start}/{end}")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<CalendarForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result<List<CalendarForm>>> Get([FromRoute] DateTime start, [FromRoute] DateTime end)
    {
        var calendar = await _calendarService.GetCalendarEventsAsync(start, end);
        return calendar;
    }

    /// <summary>
    /// Создаёт новый календарь
    /// </summary>
    /// <param name="calendar">Новый календарь</param>
    /// <returns></returns>
    [HttpPost("{summary}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<CalendarForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result<CalendarForm>> Create([FromRoute] string summary)
    {
        var newCalendar = await _calendarService.CreateCalendar(summary);
        _logger.Log(summary, newCalendar, HttpContext, "Создан календарь.", Logging.Entities.TypeRequest.POST, Logging.Entities.ModuleAstrum.Calendar);
        return newCalendar;
    }

    /// <summary>
    /// Изменяет выбранный календарь
    /// </summary>
    /// <param name="calendar">Изменённый календарь</param>
    /// <returns></returns>
    [HttpPut("{calendarId}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<CalendarForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result<CalendarForm>> Update([FromRoute] Guid calendarId, [FromBody] CalendarForm calendar)
    {
        var updated = await _calendarService.UpdateCalendar(calendarId, calendar);
        _logger.Log(calendar, updated, HttpContext, "Обовлён календарь.", Logging.Entities.TypeRequest.PUT, Logging.Entities.ModuleAstrum.Calendar);
        return updated;
    }

    /// <summary>
    /// Удаляет календарь по его id
    /// </summary>
    /// <param name="id">id календаря</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<CalendarForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result<CalendarForm>> Delete([FromRoute] Guid id)
    {
        var deleted = await _calendarService.DeleteCalendar(id);
        _logger.Log(id, deleted, HttpContext, "Удалён календарь.", Logging.Entities.TypeRequest.DELETE, Logging.Entities.ModuleAstrum.Calendar);
        return deleted;
    }
}