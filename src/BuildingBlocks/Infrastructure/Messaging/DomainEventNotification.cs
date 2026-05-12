using MediatR;
using SharedKernel;

namespace Infrastructure.Messaging;

internal sealed class DomainEventNotification<TDomainEvent>(
    TDomainEvent domainEvent)
    : INotification
    where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}