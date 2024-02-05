using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Requests;
public record AddRentalRequest
{
    [Required]
    [DataType(DataType.Date)]
    public DateOnly StartDate { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly EndDate { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid VehicleId { get; set; }
}
