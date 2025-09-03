using Application.Abstraction;
using Application.Abstraction.Utils;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Abstraction.DomainEvent;
using Domain.Entities.Orders.DomainEvents;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Application.DomainEventHandlers.Order;

public sealed class OrderConfirmationDomainEventHandler(IEmailSender emailSender, IApplicationDbContext dbContext)
    : IDomainEventHandler<OrderConfirmationDomainEvent>
{
    public async ValueTask Handle(OrderConfirmationDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == notification.UserId,cancellationToken);
        
        if (user is null)
        {
            return;
        }

        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress("eShop", "eshop@mail.com"));
        mimeMessage.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email.Value));

        mimeMessage.Subject = "Order Confirmation";
        mimeMessage.Body = new TextPart("plain")
        {
            Text =
                $"Hello {user!.FirstName},\n\nYour order with ID {notification.Order.Id} has been successfully placed.\n\nThank you for shopping with us!\n\nBest regards,\neShop Team"
        };

        await emailSender.SendAsync(mimeMessage, cancellationToken);
    }
}