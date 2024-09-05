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

internal static class VehicleMappings
{
    internal static VehicleDto MapToDto(this Domain.Entities.Car car) =>
        new()
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            DateOfProduction = car.DateOfProduction,
            NumberOfDoors = car.NumberOfDoors,
            VehicleType = car.VehicleType,
            Engine = car.Engine.Model,
        };

    internal static IEnumerable<VehicleDto> MapToDto(this IEnumerable<Domain.Entities.Car> cars) =>
        cars.Select(car => MapToDto(car));

}
