using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Persistence;
using CarRental.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Tests.Repositories;
[TestFixture]
public class GenericRepositoryTests
{
    private IGenericRepository<Car> genericRepository;
    private ApplicationDbContext dbContext;

    private readonly Guid testCarId = Guid.Parse("87679a04-87e2-4147-8f08-e3d7ae331c49");

    private IUnitOfWork unitOfWork;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("CarRentalTestDb")
            .Options;

        dbContext = new ApplicationDbContext(dbContextOptions);
        dbContext.Database.EnsureCreated();

        SeedDataBase();
    }

    [SetUp]
    public void Setup()
    {
        genericRepository = new GenericRepository<Car>(dbContext);
        unitOfWork = new UnitOfWork(dbContext);
    }

    [TearDown]
    public void CleanUp()
    {
        dbContext!.Database.EnsureDeleted();
    }

    private void SeedDataBase()
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
        dbContext.SaveChanges();
    }

    [Test]
    public async Task GivenAddingNewCarNewEntityShouldBeCreated()
    {
        // Arrange
        var car = new Car()
        {
            Brand = "TestBrand",
            Model = "TestModel",
            EngineId = Guid.Parse("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
            DateOfProduction = new DateOnly(2024, 01, 01),
            NumberOfDoors = 5
        };

        // Act
        var result = await genericRepository!.CreateAsync(car, CancellationToken.None);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        var test2 = await genericRepository.FindSingleAsync(x => x.Id == result.Id, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        });
    }

    [Test]
    public async Task GivenDeletingExistingCarShouldBeDeleted()
    {
        // Arrange
        var car = new Car()
        {
            Brand = "TestBrand",
            Model = "TestModel",
            EngineId = Guid.Parse("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
            DateOfProduction = new DateOnly(2024, 01, 01),
            NumberOfDoors = 5
        };

        var newCar = await genericRepository.CreateAsync(car, CancellationToken.None);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        if (newCar.Id == Guid.Empty)
            Assert.Fail();

        // Act
        genericRepository!.Remove(newCar);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);
        var result = await genericRepository!.FindSingleAsync(c => c.Id == newCar.Id, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GivenFindingExistingCarShouldBeFound()
    {
        // Arrange
        var car = new Car()
        {
            Brand = "TestBrand",
            Model = "TestModel",
            EngineId = Guid.Parse("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
            DateOfProduction = new DateOnly(2024, 01, 01),
            NumberOfDoors = 5
        };

        var newCar = await genericRepository!.CreateAsync(car, CancellationToken.None);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        // Act
        var result = await genericRepository!.FindSingleAsync(c => c.Id == newCar.Id, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        });
    }
}
