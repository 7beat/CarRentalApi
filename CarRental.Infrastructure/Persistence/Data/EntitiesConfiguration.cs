using CarRental.Domain.Common;
using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Persistence.Data;
internal class EnginesConfiguration : IEntityTypeConfiguration<Engine>
{
    public void Configure(EntityTypeBuilder<Engine> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.Vehicles).WithOne(v => v.Engine).HasForeignKey(v => v.EngineId);
        builder.Property(e => e.FuelType).HasConversion<string>();
        builder.HasData(new Engine()
        {
            Id = 1,
            Model = "1.9 TDI",
            Displacement = 2000,
            FuelType = FuelType.Diesel,
            Horsepower = 150
        }, new Engine()
        {
            Id = 2,
            Model = "2.0 Turbo",
            Displacement = 2200,
            FuelType = FuelType.Gasoline,
            Horsepower = 200
        }, new Engine()
        {
            Id = 3,
            Model = "KAWASAKI Z1",
            Displacement = 1000,
            FuelType = FuelType.Gasoline,
            Horsepower = 150,
        });
    }
}

internal class VehiclesConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.UseTpcMappingStrategy();
        builder.HasKey(v => v.Id);
        builder.HasOne(v => v.Engine).WithMany(e => e.Vehicles).HasForeignKey(v => v.EngineId);
    }
}

internal class CarsConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasData(new Car()
        {
            Id = 1,
            Brand = "Ford",
            Model = "Mondeo",
            DateOfProduction = new(2019, 05, 01),
            NumberOfDoors = 5,
            EngineId = 2,
        }, new()
        {
            Id = 2,
            Brand = "Volkswagen",
            Model = "Golf",
            DateOfProduction = new(2007, 04, 4),
            NumberOfDoors = 3,
            EngineId = 1
        });
    }
}

internal class MotorcyclesConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasData(new Motorcycle()
        {
            Id = 3,
            Brand = "Kawasaki",
            Model = "Ninja",
            DateOfProduction = new(2016, 03, 13),
            NumberOfWheels = 2,
            EngineId = 3
        });
    }
}