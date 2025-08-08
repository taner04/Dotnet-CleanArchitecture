using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces.DomainEvent;
using Domain.DomainEvents.User;
using MimeKit;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.DomainEventHandler
{
    [ServiceInjection(typeof(IDomainEventHandler<UserNotificationDomainEvent>), ScopeType.AddTransient)]
    internal class UserNotificationDomainEventHandler : IDomainEventHandler<UserNotificationDomainEvent>
    {
        private IEmailSender _emailSender;

        public UserNotificationDomainEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task HandleAsync(UserNotificationDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress("eShop", "eShop-buis@mail.com"));
            mimeMessage.To.Add(new MailboxAddress($"{domainEvent.FirstName} {domainEvent.LastName}", domainEvent.Email));

            mimeMessage.Subject = "Order Confirmation";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = $"Your order has been created successfully."
            };

            await _emailSender.SendAsync(mimeMessage);
        }
    }
}
