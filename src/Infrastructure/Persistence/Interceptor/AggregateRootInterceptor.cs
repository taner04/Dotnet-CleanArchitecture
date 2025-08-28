using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces;
using Infrastructure.Persistence.Extensions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptor;

public sealed class AggregateRootInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public AggregateRootInterceptor(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null) await TriggerDomainEvents(eventData.Context, cancellationToken);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
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
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}