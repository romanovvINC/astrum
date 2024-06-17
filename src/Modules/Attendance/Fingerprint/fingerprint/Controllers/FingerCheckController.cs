using FuckWeb.Data;
using FuckWeb.Features.FingerPrint;
using Microsoft.AspNetCore.Mvc;

namespace FuckWeb.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class FingerCheckController : Controller
{
    private FingerCheckService _fcService;

    public FingerCheckController(FingerCheckService fcService)
    {
        _fcService = fcService;
    }

    [Route("/fcheck")]
    [HttpGet]
    public IActionResult CheckFinger(string fingerId)
    {
        try
        {
            var resultMessage = _fcService.Check(fingerId);
            return Ok(resultMessage);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}