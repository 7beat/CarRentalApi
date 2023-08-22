using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Exceptions;
using CarRental.Application.Features.Vehicles;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Cars.Commands;
public record UpdateCarCommand : IRequest<VehicleDto>
{
    [Required]
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
}

internal class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, VehicleDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public UpdateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<VehicleDto> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.Car.FindSingleAsync(c => c.Id == request.Id, cancellationToken) ??
            throw new NotFoundException($"Resource with Id: {request.Id} was not found!");

        var carToUpdate = mapper.Map(request, car);

        unitOfWork.Car.Update(carToUpdate!);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<VehicleDto>(car);
    }
}