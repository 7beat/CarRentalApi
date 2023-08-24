using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Exceptions;
using MediatR;

namespace CarRental.Application.Features.Cars.Commands;
public record DeleteMotorcycleCommand(int Id) : IRequest<bool>;

internal class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteCarCommand, bool>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public DeleteMotorcycleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await unitOfWork.Motorcycles.FindSingleAsync(c => c.Id == request.Id, cancellationToken) ??
            throw new NotFoundException($"Resource with Id: {request.Id} was not found!");

        unitOfWork.Motorcycles.Remove(motorcycle);
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
