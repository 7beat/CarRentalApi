using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Tests.Repositories;
[TestFixture]
public class CarRepositoryTests
{
    private ICarRepository? carRepository;

    [SetUp]
    public void Setup()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("TestDb");
        var dbContext = new ApplicationDbContext(optionsBuilder.Options);

        carRepository = new CarRepository(dbContext);
    }

    [Test]
    public async Task GivenAddingCarNewCarShouldBeCreated()
    {
        var car = new Car()
        {
            Brand = "TestBrand",
            Model = "TestModel",
            DateOfProduction = new DateOnly(2020, 01, 01),
            NumberOfDoors = 5
        };

        var result = await carRepository!.CreateAsync(car, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.Brand, Is.EqualTo("TestBrand"));
            Assert.That(result.Model, Is.EqualTo("TestModel"));
        });
    }
}
