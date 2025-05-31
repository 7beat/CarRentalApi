using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Domain.Common;
using MediatR;

namespace CarRental.Application.Features.Vehicles.Queries;
public record GetAllVehiclesOfTypeQuery(VehicleType VehicleType) : IRequest<IEnumerable<VehicleDto>>;

internal class GetAllVehiclesOfTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllVehiclesOfTypeQuery, IEnumerable<VehicleDto>>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<IEnumerable<VehicleDto>> Handle(GetAllVehiclesOfTypeQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<VehicleDto>>(await unitOfWork.Vehicles.FindAllOfTypeAsync(request.VehicleType, cancellationToken));
    }
}