using SharedKernel;

namespace Application.Abstractions.Messaging;

public interface IDomainEventDispatcher
{
    Task Dispatch(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken);
}
