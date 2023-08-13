using CarRental.Application.Contracts.Persistence.IRepositories;

namespace CarRental.Application.Contracts.Persistence;
public interface IUnitOfWork
{
    IVehicleRepository Vehicle { get; }
    ICarRepository Car { get; }
    IMotorcycleRepository Motorcycles { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
