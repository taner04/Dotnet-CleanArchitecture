using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction.Entity;

public interface IAggregateRoot 
{
    void AddDomainEvent(IDomainEvent domainEvent);
    List<IDomainEvent> PopDomainEvents();
}