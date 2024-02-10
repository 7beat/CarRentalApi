using CarRental.Application.Features.Vehicles;
using System.Net.Http.Json;

namespace CarRental.IntegrationTests.CarController;
[TestFixture]
public class CarEndpointsTests
{
    private HttpClient client;

    [SetUp]
    public void Setup()
    {
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7051/")
        };
    }

    [TearDown]
    public void TearDown()
    {
        client.Dispose();
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
    [TestCase("6daa1523-ac9c-4b84-4af3-08dc19a6620a")]
    public async Task GetSingleCarsEndpointRetursSelectedCars(string carId)
    {
        // Act
        var response = await client.GetAsync($"api/Cars/GetById/{carId}");
        var car = await response.Content.ReadFromJsonAsync<VehicleDto>();
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.That(car, Is.Not.Null);
    }
}
