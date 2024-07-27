using CarRental.Api.Controllers.V2;

namespace CarRental.Api.Configuration;

public static class EndpointsConfiguration
{
    public static void RegsiterEndpoints(this WebApplication app)
    {
        app.MapCarsEndpoints();
    }
}
