using Application.Abstractions.Messaging;
using MediatR;
using SharedKernel;

namespace Infrastructure.Messaging;

public class DomainEventDispatcher(IMediator mediator)
    : IDomainEventDispatcher
{
    public async Task Dispatch(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach(var domainEvent in domainEvents)
        {
            var notification = CreateNotifcation(domainEvent);
            await mediator.Send(notification, cancellationToken);
        }
    }

    public static INotification CreateNotifcation(
        IDomainEvent domainEvent)
    {
        var notificationType = typeof(DomainEventNotification<>)
            .MakeGenericType(domainEvent.GetType());

        return (INotification)Activator.CreateInstance(notificationType, domainEvent)!;
    }
}
