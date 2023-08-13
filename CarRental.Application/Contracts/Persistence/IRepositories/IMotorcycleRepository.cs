using CarRental.Domain.Entities;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IMotorcycleRepository : IGenericRepository<Motorcycle>
{
    void Update(Motorcycle motorcycle);
}
