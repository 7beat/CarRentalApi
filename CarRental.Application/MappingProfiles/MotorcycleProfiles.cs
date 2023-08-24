using AutoMapper;
using CarRental.Application.Features.Cars.Commands;
using CarRental.Domain.Entities;

namespace CarRental.Application.MappingProfiles;
internal class MotorcycleProfiles : Profile
{
    public MotorcycleProfiles()
    {
        CreateMap<AddMotorcycleCommand, Motorcycle>();
        CreateMap<UpdateMotorcycleCommand, Motorcycle>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
