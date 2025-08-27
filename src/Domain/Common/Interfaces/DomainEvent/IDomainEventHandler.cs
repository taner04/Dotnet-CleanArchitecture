using Mediator;

namespace Domain.Common.Interfaces.DomainEvent
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent;
}
