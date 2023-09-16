using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Application.Exceptions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Rentals.Commands;
public record UpdateRentalCommand : IRequest<RentalDto>
{
    [Required]
    public Guid Id { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public Guid? VehicleId { get; init; }
}

internal class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand, RentalDto>
{
    private readonly IRentalRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public UpdateRentalCommandHandler(IRentalRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<RentalDto> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.FindSingleAsync(r => r.Id == request.Id, cancellationToken) ??
            throw new NotFoundException($"Resource with Id: {request.Id} was not found!");

        mapper.Map(request, rental);

        await repository.UpdateAsync(rental);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<RentalDto>(rental);
    }
}