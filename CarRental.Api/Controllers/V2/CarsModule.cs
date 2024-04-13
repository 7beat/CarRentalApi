using AutoMapper;
using CarRental.Application.Contracts.Requests;
using MassTransit.Mediator;

namespace CarRental.Api.Controllers.V2;

public class CarsModule(IMediator mediator, IMapper mapper) // I need to move all mapping process and mediator to here
{
    public async Task AddCarAsync(AddCarRequest request, CancellationToken cancellationToken)
    {

    }
}
