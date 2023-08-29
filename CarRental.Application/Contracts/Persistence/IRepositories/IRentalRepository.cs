using CarRental.Application.Features.Rentals;
using CarRental.Domain.Entities;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
internal interface IRentalRepository : IGenericRepository<Rental>
{
    Task<RentalDto> GetWithUserDetails(Guid id);
}
