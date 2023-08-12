using System.Text.Json.Serialization;

namespace CarRental.Application.Features.Vehicles;
public record VehicleDto
{
    public int Id { get; init; }
    public string Brand { get; init; }
    public string Model { get; init; }
    public DateOnly DateOfProduction { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? NumberOfDoors { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? NumberOfWheels { get; init; }
    public string VehicleType { get; set; }

    public string Engine { get; init; }
}
