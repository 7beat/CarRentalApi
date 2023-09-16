using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Application.Exceptions;
using MediatR;

namespace CarRental.Application.Features.Rentals.Commands;
public record DeleteRentalCommand(Guid Id) : IRequest<bool>;

internal class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand, bool>
{
    private readonly IRentalRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteRentalCommandHandler(IRentalRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.FindSingleAsync(r => r.Id == request.Id, cancellationToken) ??
            throw new NotFoundException($"Resource with Id: {request.Id} was not found!");

        repository.Remove(rental);
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}