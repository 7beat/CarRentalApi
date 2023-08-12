using AutoMapper;
using CarRental.Application.Features.Vehicles;

namespace CarRental.Application.MappingProfiles;
internal class VehicleProfiles : Profile
{
    public VehicleProfiles()
    {
        CreateMap<Domain.Entities.Car, VehicleDto>()
            .ForMember(dest => dest.Engine, opt => opt.MapFrom(src => src.Engine.Model));

        CreateMap<Domain.Entities.Motorcycle, VehicleDto>()
            .ForMember(dest => dest.Engine, opt => opt.MapFrom(src => src.Engine.Model));
    }
}
