﻿using MediatR.Playground.Model.Notifications;
using Microsoft.Extensions.Logging;

namespace MediatR.Playground.Domain.NotificationHandler;

internal class SampleNotificationFirstHandler(ILogger<SampleNotificationFirstHandler> logger)
    : INotificationHandler<SampleNotification>
{
    public Task Handle(SampleNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "A notification hit following handler: {handler} Notification Content: Id={Id};NotificationTime={EventTime}",
            nameof(SampleNotificationFirstHandler),
            notification.Id,
            notification.NotificationTime
        );

        return Task.CompletedTask;
    }
}
