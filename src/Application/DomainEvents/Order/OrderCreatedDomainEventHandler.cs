using Application.Common.Interfaces.Infrastructure;
using Domain.Common.Interfaces.DomainEvent;
using MimeKit;

namespace Application.DomainEvents.Order
{
    public sealed class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        public OrderCreatedDomainEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public async Task HandleAsync(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress("", ""));
            mimeMessage.To.Add(new MailboxAddress("", domainEvent.UserMail));

            mimeMessage.Subject = "Order Confirmation";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = $"Your order has been created successfully. Your tracking number is: {domainEvent.TrackingNumber}"
            };

            await _emailSender.SendAsync(mimeMessage);
        }
    }
}
