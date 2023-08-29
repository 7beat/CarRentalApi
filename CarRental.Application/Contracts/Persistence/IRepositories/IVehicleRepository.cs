using CarRental.Domain.Common;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> FindAllOfTypeAsync(VehicleType vehicleType, CancellationToken cancellationToken);
}
