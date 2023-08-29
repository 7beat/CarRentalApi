using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;
public class Rental : EntityBase
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public Vehicle Vehicle { get; set; }
    public Guid VehicleId { get; set; }

    public Guid UserId { get; set; }
}
