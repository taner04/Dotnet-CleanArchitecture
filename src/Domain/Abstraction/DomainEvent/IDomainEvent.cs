using Mediator;

namespace Domain.Abstraction.DomainEvent;

/// <summary>
/// Represents a domain event that can be published within the domain layer.
/// Inherits from <see cref="Mediator.INotification"/> to support notification handling.
/// </summary>
public interface IDomainEvent : INotification;