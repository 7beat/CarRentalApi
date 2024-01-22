using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Common.Commands;
public abstract record AddBaseCommand : IRequest<Guid>
{
    [Required]
    public string Brand { get; init; } = default!;
    [Required]
    public string Model { get; init; } = default!;
    [Required]
    public DateOnly DateOfProduction { get; init; }
    [Required]
    public Guid EngineId { get; set; }
    //[SwaggerSchema(ReadOnly = true)]
    public required Guid CreatedBy { get; set; }
}