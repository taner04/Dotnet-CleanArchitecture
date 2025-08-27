using Application.Common.Interfaces.Infrastructure;
using Application.DomainEvents.Order.Event;
using Domain.Common.Interfaces.DomainEvent;
using MimeKit;

namespace Application.DomainEvents.Order.Handler;

public sealed class OrderConfirmationDomainEventHandler : IDomainEventHandler<OrderConfirmationDomainEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;

    public OrderConfirmationDomainEventHandler(IEmailSender emailSender, IUnitOfWork unitOfWork)
    {
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async ValueTask Handle(OrderConfirmationDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(notification.UserId);

        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress("eShop", "eshop@mail.com"));
        mimeMessage.To.Add(new MailboxAddress($"{user!.FirstName} {user!.LastName}", user!.Email));

        mimeMessage.Subject = "Order Confirmation";
        mimeMessage.Body = new TextPart("plain")
        {
            Text =
                $"Hello {user!.FirstName},\n\nYour order with ID {notification.Order.Id} has been successfully placed.\n\nThank you for shopping with us!\n\nBest regards,\neShop Team"
        };

        await _emailSender.SendAsync(mimeMessage, cancellationToken);
    }
}