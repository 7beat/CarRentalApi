using CarRental.Application.Contracts.Identity;
using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Application.Contracts.Messaging.Services;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Infrastructure.Identity.Models;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Infrastructure.Persistence.Repositories;
using CarRental.Infrastructure.Services;
using CarRental.Infrastructure.Services.Messaging;
using CarRental.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarRental.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.ConfigureDbContext(connectionString!);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IAuthorizationHandler, RoleAuthorizationHandler>();
        services.AddTransient<IRentalMessageService, RentalMessageService>();
        services.ConfigureIdentity();
        services.ConfigureRabbitMQ();
        services.ConfigureHealthChecks(connectionString!);
    }

    private static void ConfigureDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqloptions =>
            {
                sqloptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
        });
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<ApplicationUser, IdentityRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireUppercase = false;

            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
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

    private static void ConfigureHealthChecks(this IServiceCollection services, string connectionString) =>
        services.AddHealthChecks()
        .AddSqlServer(connectionString)
        .AddRabbitMQ(rabbitConnectionString: "amqp://guest:guest@localhost:5672/");
}
