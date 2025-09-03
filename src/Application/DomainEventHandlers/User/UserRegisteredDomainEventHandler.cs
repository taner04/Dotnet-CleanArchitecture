using Application.Abstraction.Utils;
using Domain.Abstraction.DomainEvent;
using Domain.Entities.Users.DomainEvents;
using MimeKit;

namespace Application.DomainEventHandlers.User;

public sealed class UserRegisteredDomainEventHandler(IEmailSender emailSender)
    : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async ValueTask Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress("No Reply", "eShop@mail.com"));
        mimeMessage.To.Add(new MailboxAddress($"{notification.FirstName} {notification.LastName}", notification.Email));

        mimeMessage.Subject = "Welcome to eShop!";
        mimeMessage.Body = new TextPart("plain")
        {
            Text =
                $"Hello {notification.FirstName},\n\nThank you for registering with eShop! We are excited to have you on board.\n\nBest regards,\neShop Team"
        };

        await emailSender.SendAsync(mimeMessage, cancellationToken);
    }
}