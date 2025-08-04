using Domain.Common.Interfaces.DomainEvent;

namespace Domain.Common.Interfaces.Entity
{
    public interface IDomain
    {
        void AddDomainEvent(IDomainEvent domainEvent);
        List<IDomainEvent> PopDomainEvents();
    }
}
