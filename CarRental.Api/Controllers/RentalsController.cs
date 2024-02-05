using AutoMapper;
using CarRental.Application.Contracts.Requests;
using CarRental.Application.Features.Rentals.Commands;
using CarRental.Application.Features.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[SwaggerTag("Displaying and Managing Rentals")]
public class RentalsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public RentalsController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllRentalsQuery());

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetSingleRentalQuery(id));

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddRentalRequest request)
    {
        var command = mapper.Map<AddRentalCommand>(request);
        var result = await mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateRentalCommand request)
    {
        var result = await mediator.Send(request);
        return result is null ? BadRequest() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var succeded = await mediator.Send(new DeleteRentalCommand(id));
        return succeded ? NoContent() : Ok(succeded);
    }
}