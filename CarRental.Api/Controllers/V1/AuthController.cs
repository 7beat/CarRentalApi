using CarRental.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers.V1;
[SwaggerTag("User Authentication and Registering")]
public class AuthController : BaseApiController
{
    public AuthController(IMediator mediator) : base(mediator)
    { }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] LoginCommand request)
    {
        var result = await mediator.Send(request);

        return result.Succeded ? Ok(new { result.Token }) : BadRequest();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterCommand request)
    {
        var result = await mediator.Send(request);

        return result.Succeded ? StatusCode(StatusCodes.Status201Created, result.Token) : BadRequest();
    }
}
