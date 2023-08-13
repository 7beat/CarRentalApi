using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Vehicles;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries;
public record GetAllCarsQuery : IRequest<IEnumerable<VehicleDto>>;

internal class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<VehicleDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public GetAllCarsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<VehicleDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await unitOfWork.Car.FindAllAsync(cancellationToken);
        return mapper.Map<IEnumerable<VehicleDto>>(cars);
    }
}
