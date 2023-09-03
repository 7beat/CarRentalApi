using CarRental.Application.Features.Rentals.Commands;
using CarRental.Application.Features.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class RentalsController : ControllerBase
{
    private readonly IMediator mediator;

    public RentalsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllRentalsQuery());

        return result.Any() ? Ok(result) : BadRequest();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetSingleRentalQuery(id));

        return result is null ? BadRequest() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddRentalCommand request)
    {
        var result = await mediator.Send(request);
        return Ok();
    }
}