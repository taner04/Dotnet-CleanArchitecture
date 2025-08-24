using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Repositories;
using Domain.Common.Interfaces.DomainEvent;
using Domain.DomainEvents.Order;
using MimeKit;

namespace Application.DomainEventHandler
{
    public sealed class OrderConfirmationDomainEventHandler : IDomainEventHandler<OrderConfirmationDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;

        public OrderConfirmationDomainEventHandler(IEmailSender emailSender, IUnitOfWork unitOfWork)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task HandleAsync(OrderConfirmationDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(domainEvent.UserId);

            var mimeMessage = new MimeMessage();
            
            mimeMessage.From.Add(new MailboxAddress("eShop", "eshop@mail.com"));
            mimeMessage.To.Add(new MailboxAddress($"{user!.FirstName} {user!.LastName}", user!.Email));

            mimeMessage.Subject = "Order Confirmation";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = $"Hello {user!.FirstName},\n\nYour order with ID {domainEvent.Order.Id} has been successfully placed.\n\nThank you for shopping with us!\n\nBest regards,\neShop Team"
            };

            await _emailSender.SendAsync(mimeMessage, cancellationToken);
        }
    }
}
