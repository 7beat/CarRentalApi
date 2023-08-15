using CarRental.Application.Features.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly IMediator mediator;
    public CarsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCarsQuery());

        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await mediator.Send(new GetSingleCarQuery(id));

        return car is null ? NotFound() : Ok(car);
    }
}
