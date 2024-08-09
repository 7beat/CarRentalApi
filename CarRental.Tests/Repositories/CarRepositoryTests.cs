using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Persistence;
using CarRental.Persistence.Repositories;
using CarRental.UnitTests.Builders;
using Microsoft.EntityFrameworkCore;

namespace CarRental.UnitTests.Repositories;
[TestFixture]
public class CarRepositoryTests
{
    private ApplicationDbContext dbContext;
    private ICarRepository carRepository;
    private IUnitOfWork unitOfWork;

    [OneTimeSetUp]
    public void Setup()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("CarRentalTestDb");
        dbContext = new ApplicationDbContext(optionsBuilder.Options);
        SeedDataBase();
    }

    [SetUp]
    public void OneTimeSetup()
    {
        carRepository = new CarRepository(dbContext);
        unitOfWork = new UnitOfWork(dbContext);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        dbContext.Dispose();
    }

    private void SeedDataBase() // Move whole appDbContext logic to builder class. appDbContext = appDbContextHelper.BuildTestDbContext()
    {
        var testEngine = new Engine()
        {
            Id = Guid.Parse("26a79fdb-b76a-45bf-bc1b-e18487d51212"),
            CreatedAt = DateTime.Now,
            CreatedBy = Guid.Empty.ToString(),
            Displacement = 2000,
            FuelType = FuelType.Gasoline,
            Horsepower = 200,
            Model = "TestEngine"
        };
        dbContext!.Engines.Add(testEngine);
    }

    [Test]
    public async Task AddCarShouldCreateEntity()
    {
        var car = CarFaker.Car().Generate();
        Console.WriteLine();
    }

    //[Test] // Obsolete
    //public async Task GivenAddingCarNewCarShouldBeCreated()
    //{
    //    var car = new Car()
    //    {
    //        Brand = "TestBrand",
    //        Model = "TestModel",
    //        DateOfProduction = new DateOnly(2020, 01, 01),
    //        NumberOfDoors = 5,
    //        EngineId = Guid.Parse("26a79fdb-b76a-45bf-bc1b-e18487d51212"), // I need to create proper Engine for Car!!!
    //    };

    //    await carRepository!.CreateAsync(car, CancellationToken.None);
    //    await unitOfWork.SaveChangesAsync(CancellationToken.None);

    //    var result = await unitOfWork.Cars.FindSingleAsync(c => c.Id == car.Id, CancellationToken.None);

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(result, Is.Not.Null);
    //        Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
    //        Assert.That(result.Brand, Is.EqualTo("TestBrand"));
    //        Assert.That(result.Model, Is.EqualTo("TestModel"));
    //    });
    //}

    //public async Task GivenUpdatingExistingCarShouldBeUpdated()
    //{

    //}

    //public async Task GivenUpdatingNonExistingCarExceptionShouldBeThrown()
    //{

    //}

    //public async Task GivenDeletingExistingCarShouldBeDeleted()
    //{

    //}

    //public async Task GivenDeletingNonExistingCarExceptionShouldBeThrown()
    //{

    //}
}
