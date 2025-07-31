namespace Domain.Common.Interfaces
{
    public interface IEntity<TId> where TId : struct
    {
        TId Id { get; init; }
        
        void AddDomainEvent(IDomainEvent domainEvent);
        List<IDomainEvent> PopDomainEvents();
    }
}
