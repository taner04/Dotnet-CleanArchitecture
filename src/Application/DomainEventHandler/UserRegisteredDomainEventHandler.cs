using System.Diagnostics;
using Domain.Abstraction.DomainEvent;
using Domain.Entities.Users.DomainEvent;

namespace Application.DomainEventHandler;

public class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public ValueTask Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        // Here you would implement the logic to handle the event. For example, mail a notification.
        Debug.WriteLine("UserRegisteredDomainEventHandler handled an event.");
        return ValueTask.CompletedTask;
    }
}