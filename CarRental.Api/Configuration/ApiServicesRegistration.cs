using CarRental.Application;
using CarRental.Infrastructure;

namespace CarRental.Api.Configuration;

public static class ApiServicesRegistration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.RegisterApplication(configuration);
        services.RegisterInfrastructure(configuration, environment);
    }
}
