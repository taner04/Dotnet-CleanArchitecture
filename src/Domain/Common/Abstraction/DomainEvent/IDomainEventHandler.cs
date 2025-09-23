using Mediator;

namespace Domain.Common.Abstraction.DomainEvent;

/// <summary>
///     Defines a handler for domain events of type <typeparamref name="TEvent" />.
/// </summary>
/// <typeparam name="TEvent">The type of domain event to handle.</typeparam>
public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent;