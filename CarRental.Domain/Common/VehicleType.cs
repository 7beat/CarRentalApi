using System.Text.Json.Serialization;

namespace CarRental.Domain.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VehicleType
{
    All,
    Car,
    Motorcycle
}
