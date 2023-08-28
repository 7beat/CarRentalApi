using CarRental.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] LoginCommand request)
    {
        var result = await mediator.Send(request);

        return result.Succeded ? Ok(new { result.Token }) : BadRequest();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromForm] RegisterCommand request)
    {
        var result = await mediator.Send(request);

        return result.Succeded ? StatusCode(StatusCodes.Status201Created) : BadRequest();
    }
}
