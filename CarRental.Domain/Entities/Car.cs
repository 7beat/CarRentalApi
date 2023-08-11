using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;
public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    public override VehicleType VehicleType => VehicleType.Car;
}
