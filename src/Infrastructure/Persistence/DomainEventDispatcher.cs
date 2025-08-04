using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces.DomainEvent;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    [ServiceInjection(typeof(IDomainEventDispatcher), ScopeType.AddTransient)]
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(List<IDomainEvent> domainEvents)
        {
            foreach(var @event in domainEvents)
            {
                var typeHandler = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());

                var handlers = _serviceProvider.GetServices(typeHandler);

                foreach (dynamic handler in handlers)
                {
                    await handler!.Handler((dynamic)@event);
                }
            }
        }
    }
}
