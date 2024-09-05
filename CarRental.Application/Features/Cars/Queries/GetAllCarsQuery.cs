using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Vehicles;
using CarRental.Application.MappingProfiles;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries;
public record GetAllCarsQuery : IRequest<IEnumerable<VehicleDto>>;

internal class GetAllCarsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCarsQuery, IEnumerable<VehicleDto>>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    public async Task<IEnumerable<VehicleDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await unitOfWork.Cars.FindAllAsync(cancellationToken);

        return cars.MapToDto();
    }
}
