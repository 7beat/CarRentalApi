using AutoMapper;
using CarRental.Application.Contracts.Requests;
using CarRental.Application.Features.Rentals;
using CarRental.Application.Features.Rentals.Commands;
using CarRental.Application.Features.Rentals.Notifications;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Messaging.Events;

namespace CarRental.Application.MappingProfiles;
internal class RentalProfiles : Profile
{
    public RentalProfiles()
    {
        CreateMap<Rental, RentalDto>()
            .ReverseMap();

        CreateMap<AddRentalRequest, AddRentalCommand>();

        CreateMap<AddRentalCommand, Rental>();

        CreateMap<UpdateRentalCommand, Rental>()
            .ForMember(dest => dest.VehicleId, opt =>
            {
                opt.PreCondition(src => src.VehicleId is not null);
                opt.MapFrom(src => src.VehicleId!.Value);
            })
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));

        CreateMap<Rental, RentalCreatedEvent>();

        CreateMap<RentalCreatedEvent, RentalConsumedNotification>();
    }
}
