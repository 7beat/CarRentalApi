using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Infrastructure.Persistence.Repositories;
using CarRental.Persistence.Repositories;

namespace CarRental.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private bool _disposed = false;
    private readonly ApplicationDbContext dbContext;

    private ICarRepository _carRepository;
    private IVehicleRepository _vehicleRepository;
    private IMotorcycleRepository _motorcycleRepository;
    // private IRentalRepository _rentalRepository;

    public ICarRepository Cars => _carRepository ??= new CarRepository(dbContext);
    public IVehicleRepository Vehicles => _vehicleRepository ??= new VehicleRepository(dbContext);
    public IMotorcycleRepository Motorcycles => _motorcycleRepository ??= new MotorcycleRepository(dbContext);
    // public IRentalRepository Rentals => _rentalRepository ??= new RentalRepository(dbContext);

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            dbContext.Dispose();
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
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
