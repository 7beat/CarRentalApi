using CarRental.Domain.Common;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Persistence.Data;
internal static class EntitiesConfiguration
{
    public static void ConfigureEntities(this ModelBuilder builder)
    {
        builder.Entity<Vehicle>().HasKey(v => v.Id);
        builder.Entity<Vehicle>().HasOne(v => v.Engine).WithMany().HasForeignKey(v => v.EngineId);

        builder.Entity<Engine>().HasKey(e => e.Id);
        builder.Entity<Engine>().HasMany(e => e.Vehicles).WithOne(v => v.Engine).HasForeignKey(v => v.EngineId);
        builder.Entity<Engine>().Property(e => e.FuelType).HasConversion<string>();


        builder.Entity<Engine>().HasData(GetEngines());
        builder.Entity<Car>().HasData(GetCars());
        builder.Entity<Motorcycle>().HasData(GetMotorcycles());

    }

    private static IEnumerable<Engine> GetEngines()
    {
        return new List<Engine>()
        {
            new()
            {
                Id = 1,
                Model = "TestEngine",
                Horsepower = 1,
                Displacement = 1,
                FuelType = FuelType.Gasoline,
            }
        };
    }

    private static IEnumerable<Car> GetCars()
    {
        return new List<Car>()
        {
            new()
            {
                Id = 1,
                Brand = "Ford",
                Model = "Mondeo",
                DateOfProduction = new(2019, 05, 01),
                NumberOfDoors = 5,
                EngineId = 1,
            }
        };
    }

    private static IEnumerable<Motorcycle> GetMotorcycles()
    {
        return new List<Motorcycle>()
        {
            new()
            {
                Id = 2,
                Brand = "Kawasaki",
                Model = "Ninja",
                DateOfProduction = new(2016, 03, 13),
                NumberOfWheels = 2,
                EngineId = 1,
            }
        };
    }
}
