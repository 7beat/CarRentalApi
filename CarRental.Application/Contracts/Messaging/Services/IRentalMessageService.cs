﻿using CarRental.Infrastructure.Messaging.Events;

namespace CarRental.Application.Contracts.Messaging.Services;
public interface IRentalMessageService
{
    Task SendMessageAsync(RentalCreatedEvent message);
}
