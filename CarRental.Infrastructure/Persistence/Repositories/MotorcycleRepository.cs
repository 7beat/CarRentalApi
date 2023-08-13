using CarRental.Application.Contracts.Persistence.IRepositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Persistence.Data;
using CarRental.Persistence.Repositories;

namespace CarRental.Infrastructure.Persistence.Repositories;
public class MotorcycleRepository : GenericRepository<Motorcycle>, IMotorcycleRepository
{
    private ApplicationDbContext dbContext;
    public MotorcycleRepository(ApplicationDbContext context) : base(context)
    {
        dbContext = context;
    }

    public void Update(Motorcycle motorcycle)
    {
        dbContext.Update(motorcycle);
    }
}
