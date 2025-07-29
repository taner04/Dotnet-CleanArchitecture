namespace Domain.Common.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; init; }
        
        void AddDomainEvent(IDomainEvent domainEvent);
        List<IDomainEvent> PopDomainEvents();
    }
}
