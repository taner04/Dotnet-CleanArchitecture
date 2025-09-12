using System.Diagnostics;
using Domain.Abstraction.DomainEvent;
using Domain.Entities.Users.DomainEvent;

namespace Application.DomainEventHandler;

public class UserTransactionDomainEventHandler : IDomainEventHandler<UserTransactionDomainEvent>
{
    public ValueTask Handle(UserTransactionDomainEvent notification, CancellationToken cancellationToken)
    {
        // Here you would implement the logic to handle the event. For example, mail a notification.
        Debug.WriteLine("UserTransactionDomainEventHandler handled an event.");
        return ValueTask.CompletedTask;
    }
}