using Bogus;
using CarRental.Domain.Entities;

namespace CarRental.UnitTests.Builders;
public class CarFaker : Faker<Car>
{
    public static Faker<Car> Car()
    {
        return new Faker<Car>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
            .RuleFor(c => c.Model, f => f.Vehicle.Model())
            .RuleFor(c => c.NumberOfDoors, 5)
            .RuleFor(c => c.Engine, EngineFaker.Simple());
    }
}
