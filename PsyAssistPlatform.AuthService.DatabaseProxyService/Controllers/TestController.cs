using Microsoft.AspNetCore.Mvc;

namespace PsyAssistPlatform.AuthService.DatabaseProxyService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    // GET: api/Test
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test controller is working!");
    }

    // GET: api/Test/echo/{message}
    [HttpGet("echo/{message}")]
    public IActionResult Echo(string message)
    {
        return Ok($"Echo: {message}");
    }
}
