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

    public async Task<IEnumerable<Vehicle>> FindAllOfType(VehicleType vehicleType)
    {
        throw new NotImplementedException();
    }
}
