using CarRental.Application.Contracts.Identity;
using CarRental.Application.Contracts.Persistence;
using CarRental.Infrastructure.Identity.Models;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Infrastructure.Services;
using CarRental.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IAuthorizationHandler, RoleAuthorizationHandler>();
        services.ConfigureIdentity();
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
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
        var builder = services.AddIdentity<ApplicationUser, IdentityRole<int>>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireUppercase = false;

            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }
}
