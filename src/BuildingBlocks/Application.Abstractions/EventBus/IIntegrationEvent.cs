namespace Application.Abstractions.EventBus;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime OccurendOnUtc { get; }
}
