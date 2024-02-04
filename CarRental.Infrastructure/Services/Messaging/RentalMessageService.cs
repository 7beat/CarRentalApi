using CarRental.Application.Contracts.Messaging.Services;
using CarRental.Infrastructure.Messaging.Events;
using MassTransit;

namespace CarRental.Infrastructure.Services.Messaging;
public class RentalMessageService : IRentalMessageService
{
    private readonly IPublishEndpoint publishEndpoint;

    public RentalMessageService(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public async Task SendMessageAsync(RentalCreatedEvent message) =>
        await publishEndpoint.Publish(message);

}
