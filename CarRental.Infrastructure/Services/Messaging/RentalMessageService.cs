using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Application.Contracts.Messaging.Services;
using CarRental.Application.Exceptions;
using MassTransit;

namespace CarRental.Infrastructure.Services.Messaging;
public class RentalMessageService : IRentalMessageService
{
    private readonly IPublishEndpoint publishEndpoint;

    public RentalMessageService(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public async Task SendMessageAsync(RentalCreatedEvent message)
    {
        try
        {
            await publishEndpoint.Publish(message);
        }
        catch (Exception ex)
        {
            throw new BadRequestException(ex.Message, ex.InnerException);
        }

    }
}
