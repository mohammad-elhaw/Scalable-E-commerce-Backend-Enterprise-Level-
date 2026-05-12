namespace SharedKernel;

public abstract class AggregateRoot<Guid> : Entity<Guid>
{
    protected AggregateRoot() : base() { }
    protected AggregateRoot(Guid id) : base(id) { }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
    
    public void ClearDomainEvents() => _domainEvents.Clear();
}