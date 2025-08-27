using Domain.Common.Interfaces.DomainEvent;

namespace Domain.Common.Interfaces;

public interface IAggregateRoot
{
    void AddDomainEvent(IDomainEvent domainEvent);
    List<IDomainEvent> PopDomainEvents();
}