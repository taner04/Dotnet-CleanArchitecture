using Mediator;

namespace Domain.Common.Interfaces.DomainEvent;

/// <summary>
/// Defines a handler for domain events of type <typeparamref name="TEvent"/>.
/// Inherits from <see cref="INotificationHandler{TEvent}"/>.
/// </summary>
/// <typeparam name="TEvent">The type of domain event to handle.</typeparam>
public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent;