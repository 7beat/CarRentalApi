using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers.V2;

[ApiController]
[ApiVersion("2")]
[Route("api/v{apiVersion:apiVersion}/[controller]")]
[SwaggerTag("Displaying and Managing Cars")]
public class CarsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    public CarsController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpGet("[action]")]
    public IActionResult GetAll()
    {
        return Ok("Version 2 GetAll");
    }
}
