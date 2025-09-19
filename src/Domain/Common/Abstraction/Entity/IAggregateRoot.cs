using Domain.Common.Abstraction.DomainEvent;

namespace Domain.Common.Abstraction.Entity;

/// <summary>
/// Represents an aggregate root in the domain model.
/// Provides methods to manage domain events.
/// </summary>
public interface IAggregateRoot 
{
    /// <summary>
    /// Adds a domain event to the aggregate root.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    void AddDomainEvent(IDomainEvent domainEvent);

    /// <summary>
    /// Retrieves and removes all domain events from the aggregate root.
    /// </summary>
    /// <returns>A list of domain events.</returns>
    List<IDomainEvent> PopDomainEvents();
}