using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Common;
using CarRental.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Persistence.Repositories;
public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    private readonly ApplicationDbContext dbContext;

    public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Vehicle>> FindAllOfTypeAsync(VehicleType vehicleType, CancellationToken cancellationToken)
    {
        return vehicleType switch
        {
            VehicleType.Car => await dbContext.Cars.ToListAsync(cancellationToken),
            VehicleType.Motorcycle => await dbContext.Motorcycles.ToListAsync(cancellationToken),
            _ => throw new NotImplementedException($"Vehicle Type {vehicleType} isnt supported!")
        };
    }
}
