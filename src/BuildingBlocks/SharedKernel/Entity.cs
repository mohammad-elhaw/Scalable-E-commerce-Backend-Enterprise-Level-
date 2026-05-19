namespace SharedKernel;

public abstract class Entity<TId> : IHasDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public TId Id { get; protected set; } = default!;
    
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}