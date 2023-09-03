using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Application.Exceptions;
using CarRental.Application.Features.Rentals;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Identity.Models;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        var rental = await dbContext.Rentals.Include(r => r.Vehicle).FirstOrDefaultAsync(r => r.Id == id) ??
            throw new BadRequestException("Replace with NotFoundException!");

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

    public async Task<IEnumerable<RentalDto>> GetAllWithUserDetails()
    {
        var rentals = await dbContext.Rentals.Include(r => r.Vehicle).ToListAsync();

        var rentalsDto = new List<RentalDto>();

        foreach (var rental in rentals)
        {
            var user = await userManager.FindByIdAsync(rental.UserId.ToString()) ??
                throw new BadRequestException("Error");

            rentalsDto.Add(new()
            {
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                UserName = user.UserName!,
                VehicleName = $"{rental.Vehicle.Brand} {rental.Vehicle.Model}"
            });

        }

        return rentalsDto;
    }
}
