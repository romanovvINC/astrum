using FuckWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuckWeb.Controllers;


public class UserController : Controller
{
    private FDbContext _ctx;
    public UserController(FDbContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index()
    {
        var users = _ctx.Users.Include(x => x.Checks.Where(x => x.Date > DateTime.UtcNow.AddDays(-10))).ToList();
        return View(users);
    }

    /// <summary>
    /// Получение списка пользователей
    /// </summary>
    /// <returns>Список всех пользователей системы</returns>
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("/api/[controller]/[action]")]
    public IActionResult GetList() => Ok(_ctx.Users.ToList());
}