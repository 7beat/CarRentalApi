using CarRental.Application.Features.Rentals;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IRentalRepository
{
    Task<RentalDto> GetWithUserDetails(Guid id);
}
