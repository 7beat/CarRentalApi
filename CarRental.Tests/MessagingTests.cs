using AutoMapper;
using CarRental.Application.Contracts.Messaging.Events;
using CarRental.Infrastructure.MessageHandlers;
using MassTransit;
using MassTransit.Testing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarRental.UnitTests;

[TestFixture]
internal class MessagingTests
{
    [Test]
    public async Task Should_Publish_RentalCreatedEvent()
    {
        // Arrange
        await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness()
            .BuildServiceProvider(true);

        var harness = await provider.StartTestHarness();

        await using var scope = provider.CreateAsyncScope();

        // Act
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        await publishEndpoint.Publish(new RentalCreatedEvent());

        // Assert
        Assert.That(await harness.Published.Any<RentalCreatedEvent>());

        await harness.Stop();
    }

    [Test]
    public async Task Should_Consume_RentalCreatedEvent()
    {
        // Arrange
        Mock<IMediator> mediatorMock = new();
        Mock<ILogger<CreateRentalConsumer>> loggerMock = new();
        Mock<IMapper> mapperMock = new();

        await using var provider = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddSingleton(loggerMock.Object)
            .AddSingleton(mapperMock.Object)
            .AddMassTransitTestHarness(x =>
            {
                x.AddConsumer<CreateRentalConsumer>();
            })
            .BuildServiceProvider(true);

        var harness = await provider.StartTestHarness();

        await using var scope = provider.CreateAsyncScope();

        // Act
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        await publishEndpoint.Publish(new RentalCreatedEvent());

        // Assert
        Assert.Multiple(async () =>
        {
            Assert.That(await harness.Published.Any<RentalCreatedEvent>());
            Assert.That(await harness.Consumed.Any<RentalCreatedEvent>());
        });

        await harness.Stop();
    }
}
