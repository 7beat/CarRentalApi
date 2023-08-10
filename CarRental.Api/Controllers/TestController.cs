using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{

    [Authorize]
    [HttpGet("[action]")]
    public IActionResult Identity()
    {
        return Ok();
    }

}
