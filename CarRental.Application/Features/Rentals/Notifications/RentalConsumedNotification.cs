using CarRental.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRental.Application.Features.Rentals.Notifications;
public record RentalConsumedNotification(
    Guid VehicleId
    ) : INotification;

internal class RentalConsumedNotificationHandler : INotificationHandler<RentalConsumedNotification>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<RentalConsumedNotificationHandler> logger;

    public RentalConsumedNotificationHandler(IUnitOfWork unitOfWork, ILogger<RentalConsumedNotificationHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    public async Task Handle(RentalConsumedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling message in Notification for vehicle: {notification.VehicleId}");
    }
}
