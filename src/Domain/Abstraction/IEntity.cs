using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction;

public interface IEntity<TId> where TId : struct
{
    TId Id { get; init; }
    
    void AddDomainEvent(IDomainEvent domainEvent);
    IReadOnlyCollection<IDomainEvent> PopDomainEvents();
}