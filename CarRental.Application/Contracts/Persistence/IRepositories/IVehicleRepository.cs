﻿using CarRental.Domain.Common;

namespace CarRental.Application.Contracts.Persistence.IRepositories;
public interface IVehicleRepository : IGenericRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> FindAllOfTypeAsync(VehicleType vehicleType, CancellationToken cancellationToken);
    Task<Vehicle> FindSingleOfTypeAsync(int id, VehicleType vehicleType, CancellationToken cancellationToken);
}
