using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Vehicles;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries;
public record GetAllCarsQuery : IRequest<IEnumerable<VehicleDto>>;

internal class GetAllCarsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCarsQuery, IEnumerable<VehicleDto>>
{
    public async Task<IEnumerable<VehicleDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await unitOfWork.Cars.FindAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<VehicleDto>>(cars);
    }
}
