using Domain.Common.Interfaces.DomainEvent;
using Mediator;

namespace Infrastructure.Persistence.Extensions;

public static class MediatorExtension
{
    public static void PublishDomainEvents(this IMediator mediator, List<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            mediator.Publish(domainEvent, cancellationToken).GetAwaiter().GetResult();
        }
    }
}