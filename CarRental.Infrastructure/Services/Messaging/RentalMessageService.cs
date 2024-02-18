using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Application.Contracts.Messaging.Services;
using CarRental.Application.Contracts.Persistence;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CarRental.Infrastructure.Services.Messaging;
public class RentalMessageService(ILogger<RentalMessageService> logger, IPublishEndpoint publishEndpoint,
    IUnitOfWork unitOfWork) : IRentalMessageService
{
    public async Task SendMessageAsync(RentalCreatedEvent message, CancellationToken cancellationToken = default)
    {
        try
        {
            await publishEndpoint.Publish(message);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, ex.InnerException?.Message);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
