using Bogus;
using CarRental.Domain.Entities;

namespace CarRental.UnitTests.Builders;
public class EngineFaker : Faker<Engine>
{
    public static Faker<Engine> Simple()
    {
        return new Faker<Engine>()
            .RuleFor(e => e.Id, f => f.Random.Guid())
            .RuleFor(e => e.FuelType, f => f.Random.Enum<FuelType>())
            .RuleFor(e => e.Horsepower, f => f.Random.Number(100, 300))
            .RuleFor(e => e.Model, "TestEngine");
    }
}
