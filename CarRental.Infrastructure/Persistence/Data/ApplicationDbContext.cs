﻿using CarRental.Domain.Common;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Identity.Models;
using CarRental.Utility.Converters;
using MassTransit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarRental.Infrastructure.Persistence.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Engine> Engines { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.AddInboxStateEntity();
        builder.AddOutboxMessageEntity();
        builder.AddOutboxStateEntity();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DbDateOnlyConverter>()
            .HaveColumnType("date");
    }
}
