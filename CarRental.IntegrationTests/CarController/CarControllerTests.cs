using CarRental.Application.Contracts.Requests;
using CarRental.Application.Features.Vehicles;
using System.Net.Http.Json;

namespace CarRental.IntegrationTests.CarController;
[TestFixture]
public class CarControllerTests
{
    private HttpClient client;
    private CarRentalApplicationFactory carRentalApplicationFactory;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        carRentalApplicationFactory = new CarRentalApplicationFactory();
        client = carRentalApplicationFactory.CreateClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        client.Dispose();
        carRentalApplicationFactory.Dispose();
    }

    [Test]
    public async Task GetAllCarsEndpointRetursAllCars()
    {
        // Act
        var response = await client.GetAsync("api/Cars/GetAll");
        var cars = await response.Content.ReadFromJsonAsync<IEnumerable<VehicleDto>>();
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(cars);
        Assert.That(cars.Count, Is.GreaterThan(0));
    }

    [Test]
    [TestCase("ead2ec73-e5f0-4ab2-be3a-e9c0a043017d")]
    public async Task GetSingleCarsEndpointRetursSelectedCars(string carId)
    {
        // Act
        var response = await client.GetAsync($"api/Cars/GetById/{carId}");
        var car = await response.Content.ReadFromJsonAsync<VehicleDto>();
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(car, Is.Not.Null);
    }

    [Test]
    public async Task GetAllCarsAllShouldBeReturned()
    {
        // Act
        var response = await client.GetAsync("/api/Cars/GetAll");
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<VehicleDto>>();

        // Assert
        response.EnsureSuccessStatusCode();
        CollectionAssert.AllItemsAreNotNull(result);
    }

    [Test]
    public async Task CreateCarEntityShouldBeCreated()
    {
        // Arrange
        var request = new AddCarRequest()
        {
            Brand = "TestBrand",
            Model = "TestModel",
            NumberOfDoors = 3,
            DateOfProduction = new(2024, 01, 01),
            EngineId = Guid.Parse("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
        };

        // Act
        var response = await client.PostAsJsonAsync("api/Cars/Add", request);

        var carId = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(carId, Is.Not.EqualTo(Guid.Empty.ToString()));
    }
}