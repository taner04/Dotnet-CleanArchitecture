using Mediator;

namespace Domain.Abstraction.DomainEvent;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent { }