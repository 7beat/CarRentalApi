using CarRental.Domain.Entities;

namespace CarRental.Domain.Common;
public abstract class Vehicle : EntityBase
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateOnly DateOfProduction { get; set; }
    public abstract VehicleType VehicleType { get; }

    public Guid EngineId { get; set; }
    public Engine Engine { get; set; }
}
