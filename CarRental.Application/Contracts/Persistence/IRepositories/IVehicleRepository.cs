using CarRental.Domain.Common;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> FindAllOfType(VehicleType vehicleType);
    Task<Vehicle> FindSingleOfType(int id, VehicleType vehicleType);
}
