using CarRental.Domain.Common;
using System.Text.Json.Serialization;

namespace CarRental.Application.Features.Vehicles;
public record VehicleDto
{
    public Guid Id { get; init; }
    public string Brand { get; init; } = default!;
    public string Model { get; init; } = default!;
    public DateOnly DateOfProduction { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? NumberOfDoors { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? NumberOfWheels { get; init; }
    public VehicleType VehicleType { get; init; }

    public string Engine { get; init; } = default!;
}
