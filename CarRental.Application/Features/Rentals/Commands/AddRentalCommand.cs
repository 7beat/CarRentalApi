using AutoMapper;
using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Application.Contracts.Messaging.Services;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CarRental.Application.Features.Rentals.Commands;
public record AddRentalCommand : IRequest<Guid>
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid UserId { get; set; }
    public Guid VehicleId { get; set; }
}

public class AddRentalCommandValidator : AbstractValidator<AddRentalCommand>
{
    public AddRentalCommandValidator()
    {
        RuleFor(c => c.StartDate)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate);
    }
}

internal class AddRentalCommandHandler : IRequestHandler<AddRentalCommand, Guid>
{
    private readonly IRentalRepository rentalRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRentalMessageService rentalMessageService;

    public AddRentalCommandHandler(IRentalRepository rentalRepository, IUnitOfWork unitOfWork, IMapper mapper, IRentalMessageService rentalMessageService)
    {
        this.rentalRepository = rentalRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.rentalMessageService = rentalMessageService;
    }

    public async Task<Guid> Handle(AddRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = mapper.Map<Rental>(request);

        await rentalRepository.CreateAsync(rental, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var message = mapper.Map<RentalCreatedEvent>(rental);
        await rentalMessageService.SendMessageAsync(message);

        return rental.Id;
    }
}