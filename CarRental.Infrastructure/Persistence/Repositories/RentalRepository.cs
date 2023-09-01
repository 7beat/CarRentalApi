using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Application.Exceptions;
using CarRental.Application.Features.Rentals;
using CarRental.Infrastructure.Identity.Models;
using CarRental.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Persistence.Repositories;
public class RentalRepository : IRentalRepository
{
    private readonly ApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;

    public RentalRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    public async Task<RentalDto> GetWithUserDetails(Guid id)
    {
        var rental = await dbContext.Rentals.FindAsync(id);

        var user = await userManager.FindByIdAsync(rental.UserId.ToString()) ??
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
