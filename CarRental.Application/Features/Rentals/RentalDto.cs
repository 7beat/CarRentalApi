namespace CarRental.Application.Features.Rentals;
public class RentalDto
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string VehicleName { get; set; }
    public string UserName { get; set; }
}
