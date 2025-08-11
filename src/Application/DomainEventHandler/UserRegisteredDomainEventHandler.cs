using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces.DomainEvent;
using Domain.DomainEvents.User;
using MimeKit;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.DomainEventHandler
{
    [ServiceInjection(typeof(IDomainEventHandler<UserRegisteredDomainEvent>), ScopeType.AddTransient)]
    public sealed class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        public UserRegisteredDomainEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public async Task HandleAsync(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress("No Reply", "eShop@mail.com"));
            mimeMessage.To.Add(new MailboxAddress($"{domainEvent.FirstName} {domainEvent.LastName}", domainEvent.Email));

            mimeMessage.Subject = "Welcome to eShop!";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = $"Hello {domainEvent.FirstName},\n\nThank you for registering with eShop! We are excited to have you on board.\n\nBest regards,\neShop Team"
            };

            await _emailSender.SendAsync(mimeMessage, cancellationToken);
        }
    }
}
