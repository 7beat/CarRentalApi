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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CarRental.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.ConfigureDbContext(connectionString!);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IAuthorizationHandler, RoleAuthorizationHandler>();
        services.AddTransient<IRentalMessageService, RentalMessageService>();
        services.ConfigureIdentity();
        services.ConfigureHealthChecks(connectionString!, environment.IsDevelopment() ? "localhost" : "rabbitmq");

        if (environment.IsDevelopment())
        {
            services.ConfigureRabbitMQ("localhost");
        }
        else
        {
            services.ConfigureRabbitMQ("rabbitmq");
        }
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
        services.AddIdentityCore<ApplicationUser>(o =>
        {
            o.User.RequireUniqueEmail = true;
            o.Password.RequireDigit = false;
            o.Password.RequireUppercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager<SignInManager<ApplicationUser>>()
        .AddDefaultTokenProviders();
    }

    private static void ConfigureRabbitMQ(this IServiceCollection services, string host)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.AddConsumers(Assembly.GetExecutingAssembly());
            config.AddEntityFrameworkOutbox<ApplicationDbContext>(x =>
            {
                x.UseSqlServer();
                x.UseBusOutbox();
            });
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host);

                cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                cfg.UseMessageRetry(r => r.Immediate(5));

                cfg.ConfigureEndpoints(context);
                cfg.Publish<RentalCreatedEvent>();
            });
        });
    }

    private static void ConfigureHealthChecks(this IServiceCollection services, string connectionString, string host) =>
        services.AddHealthChecks()
        .AddSqlServer(connectionString)
        .AddRabbitMQ(rabbitConnectionString: $"amqp://guest:guest@{host}:5672/");
}
