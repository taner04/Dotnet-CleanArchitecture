using Application.Common.Interfaces.Infrastructure;
using Application.DomainEvents.User.Event;
using Domain.Common.Interfaces.DomainEvent;
using MimeKit;

namespace Application.DomainEvents.User.Handler
{
    public sealed class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        public UserRegisteredDomainEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public async ValueTask Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress("No Reply", "eShop@mail.com"));
            mimeMessage.To.Add(new MailboxAddress($"{notification.FirstName} {notification.LastName}", notification.Email));

            mimeMessage.Subject = "Welcome to eShop!";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = $"Hello {notification.FirstName},\n\nThank you for registering with eShop! We are excited to have you on board.\n\nBest regards,\neShop Team"
            };

            await _emailSender.SendAsync(mimeMessage, cancellationToken);
        }
    }
}
