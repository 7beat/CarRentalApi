using AutoMapper;
using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Application.Features.Rentals.Notifications;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRental.Infrastructure.MessageHandlers;

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
        logger.LogInformation($"Consuming message for vehicle: {context.Message.VehicleId}");
        var command = mapper.Map<RentalConsumedNotification>(context.Message);
        await mediator.Publish(command);
    }
}
