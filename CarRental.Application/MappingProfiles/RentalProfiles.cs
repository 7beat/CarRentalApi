using AutoMapper;
using CarRental.Application.Features.Rentals;
using CarRental.Domain.Entities;

namespace CarRental.Application.MappingProfiles;
internal class RentalProfiles : Profile
{
    public RentalProfiles()
    {
        CreateMap<Rental, RentalDto>()
            .ReverseMap();
    }
}
