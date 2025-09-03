using Application.Abstraction;
using Application.Abstraction.Utils;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Abstraction.DomainEvent;
using Domain.Entities.Orders.DomainEvents;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.DomainEventHandlers.Order;

public sealed class OrderCancelledDomainEventHandler(IApplicationDbContext dbContext)
    : IDomainEventHandler<OrderCancelledDomainEvent>
{
    public async ValueTask Handle(OrderCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var (productId, quantity) in notification.Products)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
            if (product is null)
            {
                continue;
            }

            product.IncreaseStock(quantity);
            dbContext.Products.Update(product);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}