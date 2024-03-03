using AutoMapper;
using CarRental.Application.Contracts.Requests;
using CarRental.Application.Features.Cars.Commands;
using CarRental.Application.Features.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace CarRental.Api.Controllers;
[ApiController]
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
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCarsQuery());

        return result is null ? NoContent() : Ok(result);
    }

    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var car = await mediator.Send(new GetSingleCarQuery(id));

        return car is null ? NotFound() : Ok(car);
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> Add(AddCarRequest request)
    {
        var command = mapper.Map<AddCarRequest, AddCarCommand>(request);
        command.CreatedBy = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var carId = await mediator.Send(command);
        return Ok(carId);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromForm] UpdateCarCommand request)
    {
        var car = await mediator.Send(request);
        return car is null ? BadRequest() : Ok(car);
    }

    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var succeded = await mediator.Send(new DeleteCarCommand(id));
        return succeded ? NoContent() : Ok(succeded);
    }
}
