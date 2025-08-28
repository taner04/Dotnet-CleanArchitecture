using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction;

public interface IAggregateRoot
{
    void AddDomainEvent(IDomainEvent domainEvent);
    List<IDomainEvent> PopDomainEvents();
}