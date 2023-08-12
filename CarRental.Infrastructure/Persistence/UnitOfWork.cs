using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Persistence.Repositories;

namespace CarRental.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private bool _disposed = false;
    private readonly ApplicationDbContext _dbContext;

    private ICarRepository _carRepository;
    private IVehicleRepository _vehicleRepository;

    public ICarRepository Car => _carRepository ??= new CarRepository(_dbContext);
    public IVehicleRepository Vehicle => _vehicleRepository ??= new VehicleRepository(_dbContext);

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            _dbContext.Dispose();
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
