using Domain.Common.Interfaces;
using Domain.Common.Interfaces.DomainEvent;

namespace Domain.Common.Base
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : struct
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public List<IDomainEvent> PopDomainEvents()
        {
            var domainEvents = _domainEvents.ToList();
            _domainEvents.Clear();

            return domainEvents;
        }
    }
}
