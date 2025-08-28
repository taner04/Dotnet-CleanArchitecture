using Domain.Entities.Base;
using Domain.Entities.Products;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.Orders;

public sealed class OrderItem : Entity<OrderItemId>
{
#pragma warning disable CS8618
    private OrderItem()
    {
    } // for EF
#pragma warning restore CS8618

    private OrderItem(OrderItemId id, OrderId orderId, ProductId productId, int quantity, Money unitPrice)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static OrderItem TryCreate(OrderItemId id, OrderId orderId, ProductId productId, int quantity,
        Money unitPrice)
    {
        if (id == Guid.Empty || productId == Guid.Empty || orderId == Guid.Empty) throw new InvalidIdException();

        if (quantity <= 0) throw new ValueBelowMinimumException("Quantity must be greater than zero.");

        if (unitPrice.Value <= 0) throw new ValueBelowMinimumException("Unit price must be greater than zero.");

        return new OrderItem(id, orderId, productId, quantity, unitPrice);
    }

    public OrderId OrderId { get; private set; }
    public Product Product { get; private set; } = null!; // Navigation property
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    public Money TotalPrice => Money.From(Quantity * UnitPrice.Value);
}