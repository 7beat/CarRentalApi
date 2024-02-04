using AutoMapper;
using CarRental.Infrastructure.Messaging.Events;
using MassTransit;
using MediatR;

namespace CarRental.Api.MessageHandlers;

public class CreateRentalConsumer : IConsumer<RentalCreatedEvent>
{
    private readonly ILogger<CreateRentalConsumer> logger;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public CreateRentalConsumer(IMediator mediator, ILogger<CreateRentalConsumer> logger, IMapper mapper)
    {
        this.mediator = mediator;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task Consume(ConsumeContext<RentalCreatedEvent> context)
    {
        // 1. Create in App.Features Notifications Folder and Create INotification and mediator.Publish()
        logger.LogInformation($"Consuming message for vehicle: {context.Message.VehicleId}");
        //var command = mapper.Map<RentalConsumed>(context.Message);
        //await mediator.Publish(command);
    }
}
