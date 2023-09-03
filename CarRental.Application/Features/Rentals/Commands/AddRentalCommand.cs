using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Rentals.Commands;
public record AddRentalCommand : IRequest<Guid>
{
    [Required]
    [DataType(DataType.Date)]
    public DateOnly StartDate { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly EndDate { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid VehicleId { get; set; }
}

internal class AddRentalCommandHandler : IRequestHandler<AddRentalCommand, Guid>
{
    private readonly IRentalRepository rentalRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AddRentalCommandHandler(IRentalRepository rentalRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.rentalRepository = rentalRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(AddRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = mapper.Map<Rental>(request);

        await rentalRepository.CreateAsync(rental, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return rental.Id;
    }
}