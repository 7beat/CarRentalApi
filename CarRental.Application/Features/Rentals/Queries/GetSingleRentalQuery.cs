using CarRental.Application.Contracts.Persistence.IRepositories;
using MediatR;

namespace CarRental.Application.Features.Rentals.Queries;
public record GetSingleRentalQuery(Guid Id) : IRequest<RentalDto>;

internal class GetSingleRentalQueryHandler : IRequestHandler<GetSingleRentalQuery, RentalDto>
{
    private readonly IRentalRepository rentalRepository;

    public GetSingleRentalQueryHandler(IRentalRepository rentalRepository)
    {
        this.rentalRepository = rentalRepository;
    }

    public async Task<RentalDto> Handle(GetSingleRentalQuery request, CancellationToken cancellationToken)
    {
        return await rentalRepository.GetWithUserDetails(request.Id);
    }
}
