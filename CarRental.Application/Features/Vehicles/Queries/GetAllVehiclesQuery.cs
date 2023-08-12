using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using MediatR;

namespace CarRental.Application.Features.Vehicles.Queries;
public record GetAllVehiclesQuery : IRequest<IEnumerable<VehicleDto>>;

internal class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, IEnumerable<VehicleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllVehiclesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<VehicleDto>>(await _unitOfWork.Vehicle.FindAllAsync(cancellationToken));
    }
}
