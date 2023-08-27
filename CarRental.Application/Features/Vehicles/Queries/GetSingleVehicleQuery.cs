using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using MediatR;

namespace CarRental.Application.Features.Vehicles.Queries;
public record GetSingleVehicleQuery(Guid Id) : IRequest<VehicleDto>;

internal class GetSingleVehicleQueryHandler : IRequestHandler<GetSingleVehicleQuery, VehicleDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetSingleVehicleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VehicleDto> Handle(GetSingleVehicleQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await _unitOfWork.Vehicle.FindSingleAsync(v => v.Id == request.Id, cancellationToken);
        return _mapper.Map<VehicleDto>(vehicle);
    }
}
