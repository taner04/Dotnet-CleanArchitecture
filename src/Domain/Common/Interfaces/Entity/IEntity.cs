using Domain.Common.Interfaces.DomainEvent;

namespace Domain.Common.Interfaces.Entity
{
    public interface IEntity<TId> where TId : struct
    {
        TId Id { get; init; }

        void AddDomainEvent(IDomainEvent domainEvent);
        List<IDomainEvent> PopDomainEvents();
    }
}
