using Domain.Common.Interfaces.DomainEvent;

namespace Application.Common.Interfaces.Infrastructure
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(List<IDomainEvent> domainEvents);
    }
}
