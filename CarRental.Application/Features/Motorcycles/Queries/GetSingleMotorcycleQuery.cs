using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Vehicles;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries;
public record GetSingleMotorcycleQuery(int Id) : IRequest<VehicleDto>;

internal class GetSingleMotorcycleQueryHandler : IRequestHandler<GetSingleMotorcycleQuery, VehicleDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public GetSingleMotorcycleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<VehicleDto> Handle(GetSingleMotorcycleQuery request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.Motorcycles.FindSingleAsync(c => c.Id == request.Id, cancellationToken);
        return mapper.Map<VehicleDto>(car);
    }
}
