using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Requests;
public record AddCarRequest
{
    [Required]
    public string Brand { get; set; } = default!;
    [Required]
    public string Model { get; set; } = default!;
    [Required]
    public DateOnly DateOfProduction { get; set; }
    [Required]
    public int NumberOfDoors { get; set; }
    [Required]
    public Guid EngineId { get; set; }
}
