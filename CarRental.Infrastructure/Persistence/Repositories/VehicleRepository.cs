using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Common;
using CarRental.Infrastructure.Persistence.Data;

namespace CarRental.Persistence.Repositories;
public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    private ApplicationDbContext _dbContext;

    public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Vehicle>> FindAllOfTypeAsync(VehicleType vehicleType, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Vehicle> FindSingleOfTypeAsync(int id, VehicleType vehicleType, CancellationToken cancellationToken)
    {
        Vehicle? vehicle = null;

        switch (vehicleType)
        {
            case VehicleType.Car:
                vehicle = await _dbContext.Cars.FindAsync(id, cancellationToken);
                break;
            case VehicleType.Motorcycle:
                vehicle = await _dbContext.Motorcycles.FindAsync(id, cancellationToken);
                break;
            default:
                break;
        }

        return vehicle;
    }
}
