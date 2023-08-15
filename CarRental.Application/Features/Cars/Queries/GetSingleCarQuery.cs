﻿using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Vehicles;
using MediatR;

namespace CarRental.Application.Features.Cars.Queries;
public record GetSingleCarQuery(int Id) : IRequest<VehicleDto>;

internal class GetSingleCarQueryHandler : IRequestHandler<GetSingleCarQuery, VehicleDto>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public GetSingleCarQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<VehicleDto> Handle(GetSingleCarQuery request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.Car.FindSingleAsync(c => c.Id == request.Id, cancellationToken);
        return mapper.Map<VehicleDto>(car);
    }
}