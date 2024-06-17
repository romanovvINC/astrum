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

namespace Astrum.Calendar.Controllers;

//[Authorize(Roles = "admin")]
[Area("Calendar")]
[Route("[area]/[controller]")]
public class EventController : ApiBaseController
{
    private readonly ICalendarEventService _eventService;
    private readonly ILogHttpService _logger;

    public EventController(ICalendarEventService eventService, ILogHttpService logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    /// <summary>
    ///     Создаёт новое событие в одном из календарей
    /// </summary>
    /// <param name="ev">Новое событие</param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<EventForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result<EventForm>> Create([FromBody] EventForm ev)
    {
        var response = await _eventService.CreateEvent(ev);
        _logger.Log(ev, response, HttpContext, "Создано событие.", Logging.Entities.TypeRequest.POST, Logging.Entities.ModuleAstrum.Calendar);
        return response;
        //return Json(newEvent);
    }

    /// <summary>
    ///     Изменяет существующее событие
    /// </summary>
    /// <param name="ev">Изменённое событие</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<EventForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result<EventForm>> Update([FromRoute]Guid id, [FromBody] EventForm ev)
    {
        var response = await _eventService.UpdateEvent(id, ev);
        _logger.Log(ev, response, HttpContext, "Обновлено событие.", Logging.Entities.TypeRequest.PUT, Logging.Entities.ModuleAstrum.Calendar);
        return response;
    }

    /// <summary>
    ///     Удаляет событие из определённого календаря
    /// </summary>
    /// <param name="calendarId">id календаря</param>
    /// <param name="eventId">id события</param>
    /// <returns></returns>
    [HttpDelete("{eventId}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SharedLib.Common.Results.Result<EventForm>), StatusCodes.Status200OK)]
    public async Task<SharedLib.Common.Results.Result<EventForm>> Delete([FromRoute] Guid eventId)
    {
        var response = await _eventService.DeleteEvent(eventId);
        _logger.Log(eventId, response, HttpContext, "Событие удалено.", Logging.Entities.TypeRequest.DELETE, Logging.Entities.ModuleAstrum.Calendar);
        return response;
    }
}