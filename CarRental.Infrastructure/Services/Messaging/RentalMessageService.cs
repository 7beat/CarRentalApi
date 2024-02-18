using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Application.Contracts.Messaging.Services;
using CarRental.Application.Contracts.Persistence;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CarRental.Infrastructure.Services.Messaging;
public class RentalMessageService : IRentalMessageService
{
    private readonly ILogger<RentalMessageService> logger;
    private readonly IPublishEndpoint publishEndpoint;
    private readonly IUnitOfWork unitOfWork;

    public RentalMessageService(ILogger<RentalMessageService> logger, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
    {
        this.logger = logger;
        this.publishEndpoint = publishEndpoint;
        this.unitOfWork = unitOfWork;
    }

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
