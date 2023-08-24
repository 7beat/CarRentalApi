using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Vehicles;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries;
public record GetAllMotorcyclesQuery : IRequest<IEnumerable<VehicleDto>>;

internal class GetAllMotorcyclesQueryHandler : IRequestHandler<GetAllMotorcyclesQuery, IEnumerable<VehicleDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public GetAllMotorcyclesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<VehicleDto>> Handle(GetAllMotorcyclesQuery request, CancellationToken cancellationToken)
    {
        var cars = await unitOfWork.Motorcycles.FindAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<VehicleDto>>(cars);
    }
}
