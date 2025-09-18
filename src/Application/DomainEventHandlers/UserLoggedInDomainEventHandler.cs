using System.Diagnostics;
using Domain.Abstraction.DomainEvent;
using Domain.Entities.Users.DomainEvents;

namespace Application.DomainEventHandlers;

public class UserLoggedInDomainEventHandler : IDomainEventHandler<UserLoggedInDomainEvent>
{
    public ValueTask Handle(UserLoggedInDomainEvent notification, CancellationToken cancellationToken)
    {
        // Here you would implement the logic to handle the event. For example, mail a notification.
        Debug.WriteLine(notification.User.WantsEmailNotifications
            ? "Wants email notifications"
            : "Don't wants email notifications");

        Debug.WriteLine("UserLoggedInDomainEventHandler handled an event.");
        return ValueTask.CompletedTask;
    }
}