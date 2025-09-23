using Domain.Common.Abstraction.Entity;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors;

public sealed class AggregateRootInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            await TriggerDomainEvents(eventData.Context, cancellationToken);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task TriggerDomainEvents(DbContext context, CancellationToken cancellationToken)
    {
        var aggregateRoots = context.ChangeTracker
            .Entries()
            .Where(e => e.Entity is IAggregateRoot)
            .ToList();

        foreach (var entry in aggregateRoots)
        {
            if (entry.Entity is not IAggregateRoot aggregateRoot)
            {
                continue;
            }

            var domainEvents = aggregateRoot.PopDomainEvents();
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}