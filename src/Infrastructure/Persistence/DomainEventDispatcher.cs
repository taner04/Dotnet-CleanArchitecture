using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces.DomainEvent;

namespace Infrastructure.Persistence
{
    [ServiceInjection(typeof(IDomainEventDispatcher), ScopeType.AddTransient)]
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), "Service provider cannot be null.");
        }
        
        public async Task DispatchAsync(List<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handler = _serviceProvider.GetService(handlerType);

                if (handler is not null)
                {
                    await ((dynamic)handler).HandleAsync((dynamic)domainEvent, cancellationToken);
                }
            }
        }
    }
}
