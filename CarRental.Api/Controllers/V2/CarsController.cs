using Asp.Versioning;
using CarRental.Application.Features.Cars.Queries;
using CarRental.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers.V2;

[ApiController]
[ApiVersion(2)]
[SwaggerTag("Displaying and Managing Cars")]
public class CarsController : BaseApiController
{
    public CarsController(IMediator mediator) : base(mediator)
    { }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<Car>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCarsQuery());

        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> GetById([SwaggerParameter("Id of Car")] Guid id)
    {
        var car = await mediator.Send(new GetSingleCarQuery(id));

        return car is null ? NotFound() : Ok(car);
    }
}