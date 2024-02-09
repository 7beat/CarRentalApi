namespace CarRental.Application.Contracts.Messaging.Events;
public class RentalCreatedEvent
{
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateOfCreation { get; init; } = DateTime.Now;
}
