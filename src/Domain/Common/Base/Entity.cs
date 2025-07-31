using Domain.Common.Interfaces;

namespace Domain.Common.Base
{
    public abstract class Entity<TId> : Auditable, IEntity<TId> where TId : struct
    {
        public TId Id { get; init; }

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
