using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure.Persistence;

public abstract class ModuleDbContext(
    DbContextOptions<ModuleDbContext> options,
    IDomainEventDispatcher dispatcher)
    : DbContext(options)
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker
            .Entries<AggregateRoot<Guid>>()
            .Select(e => e.Entity)
            .SelectMany(e => e.DomainEvents)
            .ToList();

        foreach (var entry in ChangeTracker.Entries<AggregateRoot<Guid>>())
        {
            entry.Entity.ClearDomainEvents();
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents.Count != 0 && dispatcher != null)
            await dispatcher.Dispatch(domainEvents, cancellationToken);

        return result;
    }
}
