using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Common.Commands;
using CarRental.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CarRental.Tests")]
namespace CarRental.Application.Features.Cars.Commands;
public record AddCarCommand : AddBaseCommand
{
    public int NumberOfDoors { get; init; }
}

public class AddCarCommandValidator : AddBaseValidator<AddCarCommand>
{
    public AddCarCommandValidator()
    {
        RuleFor(c => c.NumberOfDoors)
            .NotEmpty()
            .LessThanOrEqualTo(5);
    }
}

internal class AddCarCommandHandler : IRequestHandler<AddCarCommand, Guid>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public AddCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        var car = mapper.Map<Car>(request);

        car = await unitOfWork.Cars.CreateAsync(car, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return car.Id;
    }
}
