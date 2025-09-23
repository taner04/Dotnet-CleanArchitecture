using Mediator;

namespace Domain.Common.Abstraction.DomainEvent;

/// <summary>
///     Represents a domain event in the system.
///     Implement this interface for events that should be handled by the Mediator.
/// </summary>
public interface IDomainEvent : INotification;