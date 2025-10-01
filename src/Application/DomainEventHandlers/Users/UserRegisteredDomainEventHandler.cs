using System.Diagnostics;
using Domain.Common.Abstraction.DomainEvent;
using Domain.Entities.ApplicationUsers.DomainEvents;

namespace Application.DomainEventHandlers.Users;

public class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public ValueTask Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        // Here you would implement the logic to handle the event. For example, mail a notification.
        Debug.WriteLine(notification.ApplicationUser.WantsEmailNotifications
            ? "Wants email notifications"
            : "Don't wants email notifications");

        Debug.WriteLine("UserRegisteredDomainEventHandler handled an event.");
        return ValueTask.CompletedTask;
    }
}