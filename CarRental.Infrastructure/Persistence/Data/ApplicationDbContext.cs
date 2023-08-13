using CarRental.Domain.Common;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Identity.Models;
using CarRental.Utility.Converters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarRental.Infrastructure.Persistence.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Engine> Engines { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DbDateOnlyConverter>()
            .HaveColumnType("date");
    }
}
