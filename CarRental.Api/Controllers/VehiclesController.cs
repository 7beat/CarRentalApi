using CarRental.Application.Features.Vehicles.Queries;
using CarRental.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("[controller]")]
[SwaggerTag("Displaying all available vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator mediator;
    public VehiclesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllVehiclesQuery());
        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("[action]/{vehicleType}")]
    public async Task<IActionResult> GetAllOfType([FromRoute] VehicleType vehicleType)
    {
        var t = await mediator.Send(new GetAllVehiclesOfTypeQuery(vehicleType));
        return Ok(t);
    }
}
