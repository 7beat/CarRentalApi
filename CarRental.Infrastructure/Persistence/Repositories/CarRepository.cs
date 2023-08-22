using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Persistence.Data;

namespace CarRental.Persistence.Repositories;
public class CarRepository : GenericRepository<Car>, ICarRepository
{
    private ApplicationDbContext _dbContext;

    public CarRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Car car)
    {
        _dbContext.Cars.Update(car);
    }
}
