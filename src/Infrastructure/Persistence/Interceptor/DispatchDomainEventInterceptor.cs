using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptor
{
    public sealed class DispatchDomainEventInterceptor : SaveChangesInterceptor
    {
        IDomainEventDispatcher _domainEventDispatcher;
        
        public DispatchDomainEventInterceptor(IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                await DispatchDomainEvents(eventData.Context);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext context)
        {
            var domainEntities = context.ChangeTracker.Entries<IDomain>()
                .SelectMany(d =>
                {
                    var domainEvents = d.Entity.PopDomainEvents();
                    return domainEvents;
                })
                .ToList();

            await _domainEventDispatcher.DispatchAsync(domainEntities);
        }
    }
}
