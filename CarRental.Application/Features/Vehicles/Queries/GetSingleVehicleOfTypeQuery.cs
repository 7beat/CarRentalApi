using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Domain.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Vehicles.Queries;
public record GetSingleVehicleOfTypeQuery : IRequest<VehicleDto>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public VehicleType VehicleType { get; set; }
}

internal class GetSingleVehicleOfTypeQueryHandler : IRequestHandler<GetSingleVehicleOfTypeQuery, VehicleDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public GetSingleVehicleOfTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<VehicleDto> Handle(GetSingleVehicleOfTypeQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<VehicleDto>(await unitOfWork.Vehicles.FindSingleOfTypeAsync(request.Id, request.VehicleType, cancellationToken));
    }
}