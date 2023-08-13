using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Cars.Commands;
public record AddCarCommand : IRequest<int>
{
    [Required]
    public string Brand { get; init; }
    [Required]
    public string Model { get; init; }
    [Required]
    public DateOnly DateOfProduction { get; init; }
    [Required]
    public int NumberOfDoors { get; init; }
    [Required]
    public int EngineId { get; set; }
}

internal class AddCarCommandHandler : IRequestHandler<AddCarCommand, int>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public AddCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<int> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        var car = mapper.Map<Car>(request);

        car = await unitOfWork.Car.CreateAsync(car, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return car.Id;
    }
}
