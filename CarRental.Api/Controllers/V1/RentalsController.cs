using AutoMapper;
using CarRental.Application.Contracts.Requests;
using CarRental.Application.Features.Rentals.Commands;
using CarRental.Application.Features.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers.V1;
[ApiController]
[SwaggerTag("Displaying and Managing Rentals")]
public class RentalsController : BaseApiController
{
    public RentalsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    { }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllRentalsQuery());

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetSingleRentalQuery(id));

        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Add([FromForm] AddRentalRequest request)
    {
        var command = mapper.Map<AddRentalCommand>(request);
        var result = await mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromForm] UpdateRentalCommand request)
    {
        var result = await mediator.Send(request);
        return result is null ? BadRequest() : Ok(result);
    }

    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var succeded = await mediator.Send(new DeleteRentalCommand(id));
        return succeded ? NoContent() : Ok(succeded);
    }
}