using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction;

public interface IAggregateRoot
{
    void AddDomainEvent(IDomainEvent domainEvent);
    IReadOnlyCollection<IDomainEvent> PopDomainEvents();
}