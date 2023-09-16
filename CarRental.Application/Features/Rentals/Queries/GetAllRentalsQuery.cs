using AutoMapper;
using CarRental.Application.Contracts.Persistence.IRepositories;
using MediatR;

namespace CarRental.Application.Features.Rentals.Queries;
public record GetAllRentalsQuery : IRequest<IEnumerable<RentalDto>>;

internal class GetAllRentalsQueryHandler : IRequestHandler<GetAllRentalsQuery, IEnumerable<RentalDto>>
{
    private readonly IRentalRepository rentalRepository;
    private readonly IMapper mapper;

    public GetAllRentalsQueryHandler(IRentalRepository rentalRepository, IMapper mapper)
    {
        this.rentalRepository = rentalRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<RentalDto>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
    {
        var rentals = await rentalRepository.GetAllWithUserDetails();

        return mapper.Map<IEnumerable<RentalDto>>(rentals);
    }
}
