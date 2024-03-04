using CarRental.Application.Features.Vehicles.Queries;
using CarRental.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers.V1;
[ApiController]
[SwaggerTag("Displaying all available vehicles")]
public class VehiclesController : BaseApiController
{
    public VehiclesController(IMediator mediator) : base(mediator)
    { }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllVehiclesQuery());
        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("[action]/{vehicleType}")]
    public async Task<IActionResult> GetAllOfType([FromRoute][SwaggerParameter(Description = "Type of Vehicle")] VehicleType vehicleType)
    {
        var result = await mediator.Send(new GetAllVehiclesOfTypeQuery(vehicleType));
        return result is null ? NoContent() : Ok(result);
    }
}
