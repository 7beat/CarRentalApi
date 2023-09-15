﻿using AutoMapper;
using CarRental.Application.Features.Rentals;
using CarRental.Application.Features.Rentals.Commands;
using CarRental.Domain.Entities;

namespace CarRental.Application.MappingProfiles;
internal class RentalProfiles : Profile
{
    public RentalProfiles()
    {
        CreateMap<Rental, RentalDto>()
            .ReverseMap();

        CreateMap<AddRentalCommand, Rental>();

        CreateMap<UpdateRentalCommand, Rental>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
