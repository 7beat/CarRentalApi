using CarRental.Application.Features.Rentals;
using CarRental.Domain.Entities;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IRentalRepository : IGenericRepository<Rental>
{
    Task<RentalDto> GetWithUserDetails(Guid id);
    Task<IEnumerable<RentalDto>> GetAllWithUserDetails();
}
