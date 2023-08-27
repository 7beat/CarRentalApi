using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Cars.Commands;
public record AddMotorcycleCommand : IRequest<Guid>
{
    [Required]
    public string Brand { get; init; }
    [Required]
    public string Model { get; init; }
    [Required]
    public DateOnly DateOfProduction { get; init; }
    [Required]
    public int NumberOfWheels { get; init; }
    [Required]
    public int EngineId { get; set; }
}

internal class AddMotorcycleCommandHandler : IRequestHandler<AddMotorcycleCommand, Guid>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public AddMotorcycleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(AddMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = mapper.Map<Motorcycle>(request);

        motorcycle = await unitOfWork.Motorcycles.CreateAsync(motorcycle, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return motorcycle.Id;
    }
}
