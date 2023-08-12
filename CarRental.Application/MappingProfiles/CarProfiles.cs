using AutoMapper;
using CarRental.Application.Features.Cars.Commands;
using CarRental.Domain.Entities;

namespace CarRental.Application.MappingProfiles;
internal class CarProfiles : Profile
{
    public CarProfiles()
    {
        CreateMap<AddCarCommand, Car>();
    }
}
