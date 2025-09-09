using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction.Entity;

public interface IAggregateRoot<TId> : IEntity<TId>
    where TId : struct
{
    void AddDomainEvent(IDomainEvent domainEvent);
    List<IDomainEvent> PopDomainEvents();
}