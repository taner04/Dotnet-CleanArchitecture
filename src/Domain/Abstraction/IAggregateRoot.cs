using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction;

/// <summary>
/// Represents the root entity of an aggregate in the domain model.
/// Provides methods for managing domain events.
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    /// Adds a domain event to the aggregate's collection of events.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    void AddDomainEvent(IDomainEvent domainEvent);

    /// <summary>
    /// Retrieves and removes all domain events from the aggregate.
    /// </summary>
    /// <returns>
    /// A read-only collection of domain events that were raised by the aggregate.
    /// </returns>
    IReadOnlyCollection<IDomainEvent> PopDomainEvents();
}