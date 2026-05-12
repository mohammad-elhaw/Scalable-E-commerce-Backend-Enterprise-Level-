namespace Application.Abstractions.EventBus;

public abstract record IntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime OccurendOnUtc { get; init; } = DateTime.UtcNow;
}
