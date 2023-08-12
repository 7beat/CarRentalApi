using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;
public class Engine : EntityBase
{
    public string Model { get; set; }
    public int Horsepower { get; set; }
    public double Displacement { get; set; }
    public FuelType FuelType { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; }
}

public enum FuelType
{
    Gasoline,
    Diesel,
    Electric,
    Hybrid
}