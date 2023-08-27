using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Exceptions;
using CarRental.Application.Features.Vehicles;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Cars.Commands;
public record UpdateMotorcycleCommand : IRequest<VehicleDto>
{
    [Required]
    public Guid Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
}

internal class UpdateMotorcycleCommandHandler : IRequestHandler<UpdateMotorcycleCommand, VehicleDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public UpdateMotorcycleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<VehicleDto> Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await unitOfWork.Motorcycles.FindSingleAsync(c => c.Id == request.Id, cancellationToken) ??
            throw new NotFoundException($"Resource with Id: {request.Id} was not found!");

        var motorcycleToUpdate = mapper.Map(request, motorcycle);

        unitOfWork.Motorcycles.Update(motorcycleToUpdate!);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<VehicleDto>(motorcycle);
    }
}