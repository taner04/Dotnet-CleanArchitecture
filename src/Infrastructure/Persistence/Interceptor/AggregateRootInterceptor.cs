using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptor
{
    public sealed class AggregateRootInterceptor : SaveChangesInterceptor
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public AggregateRootInterceptor(IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                await TriggerDomainEvents(eventData.Context, cancellationToken);
            }

            return result;
        }

        private async Task TriggerDomainEvents(DbContext context, CancellationToken cancellationToken)
        {
            var aggregateRoots = context.ChangeTracker
                                        .Entries()
                                        .Where(e => e.Entity is IAggregateRoot);

            foreach (var entry in aggregateRoots)
            {
                if (entry.Entity is not IAggregateRoot aggregateRoot) continue;
                
                var domainEvents = aggregateRoot.PopDomainEvents();
                await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);
            }
        }
    }
}
