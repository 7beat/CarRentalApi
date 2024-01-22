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

/// <summary>
/// Model Relations has to be reworked in order to Fix RentalRepository and including User reference
/// </summary>
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
            throw new NotFoundException($"Rental with Id: \"{id}\" was not found");

        var user = await userManager.FindByIdAsync(rental.UserId.ToString()) ??
            throw new NotFoundException($"User with Id: \"{rental.UserId}\" was not found");

        return new()
        {
            StartDate = rental.StartDate,
            EndDate = rental.EndDate,
            UserName = user.UserName!,
            VehicleName = $"{rental.Vehicle.Brand} {rental.Vehicle.Model}"
        };
    }

    public async Task<IEnumerable<RentalDto>> GetAllWithUserDetails()
    {
        var rentals = await dbContext.Rentals.Include(r => r.Vehicle).ToListAsync();

        var rentalsDto = new List<RentalDto>();

        foreach (var rental in rentals)
        {
            var user = await userManager.FindByIdAsync(rental.UserId.ToString()) ??
                throw new NotFoundException($"User with Id: \"{rental.UserId}\" was not found");

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

    public override async Task<Rental> CreateAsync(Rental rental, CancellationToken cancellationToken)
    {
        if (await IsOverlap(rental))
            throw new BadRequestException("Selected Vehicle is already rented at given Time!");

        return await base.CreateAsync(rental, cancellationToken);
    }

    public async Task UpdateAsync(Rental rental)
    {
        if (await IsOverlap(rental))
            throw new BadRequestException("Selected Vehicle is already rented at given Time!");

        dbContext.Update(rental);
    }

    private async Task<bool> IsOverlap(Rental entity) =>
    await dbContext.Rentals
        .Where(r => r.VehicleId == entity.VehicleId)
        .AnyAsync(r =>
        (entity.StartDate >= r.StartDate && entity.StartDate <= r.EndDate) ||
        (entity.EndDate >= r.StartDate && entity.EndDate <= r.EndDate) ||
        (entity.StartDate <= r.StartDate && entity.EndDate >= r.EndDate));
}
