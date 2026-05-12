using Application.Abstractions.EventBus;
using DotNetCore.CAP;

namespace Infrastructure.EventBus;

internal sealed class CapEventBus(ICapPublisher capPublisher)
    : IEventBus
{
    public Task PublishAsync<TIntegrationEvent>(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent
    {
        var eventName = typeof(TIntegrationEvent).Name!;

        return capPublisher.PublishAsync(
            eventName,
            integrationEvent,
            cancellationToken: cancellationToken);
    }
}