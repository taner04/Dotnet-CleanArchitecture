using System.Collections.Immutable;
using Domain.Abstraction;
using Domain.Abstraction.DomainEvent;

namespace Domain.Entities.Base;

public abstract class AggregateRoot<TId> : Entity<TId> , IAggregateRoot
    where TId : struct
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IReadOnlyCollection<IDomainEvent> PopDomainEvents()
    {
        var domainEvents = _domainEvents.ToImmutableList();
        _domainEvents.Clear();

        return domainEvents;
    }
}