using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Application.Exceptions;
using CarRental.Application.Features.Rentals;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Identity.Models;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Persistence.Repositories;
public class RentalRepository : GenericRepository<Rental>, IRentalRepository
{
    private readonly ApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;

    public RentalRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : base(dbContext)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    public async Task<RentalDto> GetWithUserDetails(Guid id)
    {
        var rental = await dbContext.Rentals.FindAsync(id) ??
            throw new BadRequestException("Replace with NotFoundException!");

        var user = await userManager.FindByIdAsync("38bfad0a-a6f4-4cbf-828f-d5155bb5d5e9") ??
            throw new BadRequestException("Replace with NotFoundException!");

        return new()
        {
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            VehicleName = $"{rental.Vehicle.Brand} {rental.Vehicle.Model}",
            UserName = user.UserName!
        };
    }
}
