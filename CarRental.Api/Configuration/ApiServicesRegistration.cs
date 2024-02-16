using CarRental.Application;
using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Infrastructure;
using MassTransit;
using System.Reflection;

namespace CarRental.Api.Configuration;

public static class ApiServicesRegistration
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterApplication(configuration);
        services.RegisterInfrastructure(configuration);
        services.ConfigureRabbitMQ();
    }

    private static void ConfigureRabbitMQ(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.AddConsumers(Assembly.GetExecutingAssembly());
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost");

                cfg.ConfigureEndpoints(context);
                cfg.Publish<RentalCreatedEvent>();
            });
        });
    }
}
