using CarRental.Application.Contracts.Persistence.IRepositories;

namespace CarRental.Application.Contracts.Persistence;
public interface IUnitOfWork
{
    IVehicleRepository Vehicles { get; }
    ICarRepository Cars { get; }
    IMotorcycleRepository Motorcycles { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
