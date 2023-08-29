using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Exceptions;
using MediatR;

namespace CarRental.Application.Features.Cars.Commands;
public record DeleteCarCommand(Guid Id) : IRequest<bool>;

internal class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, bool>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public DeleteCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.Cars.FindSingleAsync(c => c.Id == request.Id, cancellationToken) ??
            throw new NotFoundException($"Resource with Id: {request.Id} was not found!");

        unitOfWork.Cars.Remove(car);
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
