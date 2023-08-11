using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;
public class Motorcycle : Vehicle
{
    public int NumberOfWheels { get; set; }
    public override VehicleType VehicleType => VehicleType.Motorcycle;
}
