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
            Id = Guid.Parse("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
            Model = "1.9 TDI",
            Displacement = 2000,
            FuelType = FuelType.Diesel,
            Horsepower = 150
        }, new Engine()
        {
            Id = Guid.Parse("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"),
            Model = "2.0 Turbo",
            Displacement = 2200,
            FuelType = FuelType.Gasoline,
            Horsepower = 200
        }, new Engine()
        {
            Id = Guid.Parse("5a6d7c68-b19b-4c82-94c5-43c084048092"),
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
        builder.Navigation(v => v.Engine).AutoInclude();
    }
}

internal class CarsConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasData(new Car()
        {
            Id = Guid.NewGuid(),
            Brand = "Ford",
            Model = "Mondeo",
            DateOfProduction = new(2019, 05, 01),
            NumberOfDoors = 5,
            EngineId = Guid.Parse("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"),
        });
    }
}

internal class MotorcyclesConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasData(new Motorcycle()
        {
            Id = Guid.NewGuid(),
            Brand = "Kawasaki",
            Model = "Ninja",
            DateOfProduction = new(2016, 03, 13),
            NumberOfWheels = 2,
            EngineId = Guid.Parse("5a6d7c68-b19b-4c82-94c5-43c084048092")
        });
    }
}

internal class RentalsConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasOne(r => r.Vehicle).WithMany().HasForeignKey(r => r.VehicleId);
    }
}