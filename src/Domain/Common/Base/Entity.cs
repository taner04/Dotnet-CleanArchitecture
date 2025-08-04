using Domain.Common.Interfaces.DomainEvent;
using Domain.Common.Interfaces.Entity;

namespace Domain.Common.Base
{
    public abstract class Entity<TId> : Auditable, IEntity<TId>, IDomain,ISoftDeletable where TId : struct
    {
        public TId Id { get; init; }
        public bool IsDeleted { get; set; } = false;

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
