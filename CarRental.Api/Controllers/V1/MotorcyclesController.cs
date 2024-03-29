﻿using CarRental.Application.Features.Cars.Commands;
using CarRental.Application.Features.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRental.Api.Controllers.V1;
[ApiController]
[SwaggerTag("Displaying and Managing Motorcycles")]
public class MotorcyclesController : BaseApiController
{
    public MotorcyclesController(IMediator mediator) : base(mediator)
    { }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllMotorcyclesQuery());

        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var car = await mediator.Send(new GetSingleMotorcycleQuery(id));

        return car is null ? NotFound() : Ok(car);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Add([FromForm] AddMotorcycleCommand request)
    {
        var motorcycleId = await mediator.Send(request);
        return Ok(motorcycleId);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromForm] UpdateMotorcycleCommand request)
    {
        var motorcycle = await mediator.Send(request);
        return motorcycle is null ? BadRequest() : Ok(motorcycle);
    }

    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var succeded = await mediator.Send(new DeleteMotorcycleCommand(id));
        return succeded ? BadRequest() : NoContent();
    }
}
