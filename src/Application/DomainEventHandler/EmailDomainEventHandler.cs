using Domain.Common.Interfaces.DomainEvent;

namespace Application.DomainEventHandler
{
    public abstract class EmailDomainEventHandler<TEvent> : IDomainEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        public Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
