using Ardalis.Specification;
using Domain.ValueObjects.Identifiers;

namespace Domain.Entities.Orders;

public class OrdersByUserSpecification : Specification<Order>
{
    public OrdersByUserSpecification(UserId userId)
    {
        Query.Where(order => order.UserId == userId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product);
    }
}