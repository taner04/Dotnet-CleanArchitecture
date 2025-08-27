using Mediator;

namespace Domain.Common.Interfaces.DomainEvent;

/// <summary>
/// Represents a domain event that can be published via the mediator pattern.
/// </summary>
public interface IDomainEvent : INotification;