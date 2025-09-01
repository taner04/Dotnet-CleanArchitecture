using Mediator;

namespace Domain.Abstraction.DomainEvent;

/// <summary>
/// Defines a handler for domain events of type <typeparamref name="TEvent"/>.
/// Inherits from <see cref="INotificationHandler{TEvent}"/> to integrate with the Mediator pattern.
/// </summary>
/// <typeparam name="TEvent">The type of domain event to handle.</typeparam>
public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent;