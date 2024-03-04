using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/v{apiVersion:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator mediator;
    protected readonly IMapper mapper;

    public BaseApiController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public BaseApiController(IMediator mediator) => this.mediator = mediator;
}
