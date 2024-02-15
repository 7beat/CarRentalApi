using CarRental.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[SwaggerTag("User Authentication and Registering")]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginCommand request)
    {
        var result = await mediator.Send(request);

        return result.Succeded ? Ok(new { result.Token }) : BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand request)
    {
        var result = await mediator.Send(request);

        return result.Succeded ? StatusCode(StatusCodes.Status201Created, result.Token) : BadRequest();
    }
}
