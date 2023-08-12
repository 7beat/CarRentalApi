using CarRental.Domain.Entities;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface ICarRepository : IGenericRepository<Car>
{
    void Update(Car car);
}
