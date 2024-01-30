using MediatR;

namespace CarRental.Application.Features.Common.Commands;
public abstract record AddBaseCommand : IRequest<Guid>
{
    public string Brand { get; init; } = default!;
    public string Model { get; init; } = default!;
    public DateOnly DateOfProduction { get; init; }
    public Guid EngineId { get; init; }
    public required Guid CreatedBy { get; init; }
}