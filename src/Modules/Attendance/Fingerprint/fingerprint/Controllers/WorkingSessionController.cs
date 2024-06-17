using FuckWeb.Common.Interfaces;
using FuckWeb.Features.WorkingSession;
using Microsoft.AspNetCore.Mvc;

namespace FuckWeb.Controllers;

public class WorkingSessionController : Controller
{
    private WorkingSessionService _wsService;

    public WorkingSessionController(WorkingSessionService wsService, IDateTimeService tzS)
    {
        _wsService = wsService;
        var tz = tzS.CurrentUserTime();
    }

    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index() => View(_wsService.GetTodaySessions());
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Details(long userId) => View(_wsService.GetWeeklyUserSessions(userId));
    
    /// <summary>
    /// Информация по пользователю за неделю
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("/api/[controller]/[action]")]
    public IActionResult UserWeeklySessions(long userId) => Ok(_wsService.GetWeeklyUserSessions(userId));
    

    /// <summary>
    /// Информацтя по пользователю за период
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("/api/[controller]/[action]")]
    public IActionResult UserSessions(long userId, DateTime fromDate, DateTime toDate) => Ok(_wsService.GetUserSessions(userId, fromDate.ToUniversalTime(), toDate.ToUniversalTime()));
    
    
    /// <summary>
    /// Список всех отметок в системе за период
    /// </summary>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("/api/[controller]/[action]")]
    public IActionResult GetSessions(DateTime fromDate, DateTime toDate) =>
        Ok(_wsService.GetSessions(fromDate, toDate));
}