using Domain.Entities.Base;
using Domain.Entities.Products;
using Domain.Exceptions;
using Domain.ValueObjects;
using OrderId = Domain.ValueObjects.Identifiers.OrderId;
using OrderItemId = Domain.ValueObjects.Identifiers.OrderItemId;
using ProductId = Domain.ValueObjects.Identifiers.ProductId;

namespace Domain.Entities.Orders;

public sealed class OrderItem : Entity<OrderItemId>
{
#pragma warning disable CS8618
    private OrderItem()
    {
    } // for EFC
#pragma warning restore CS8618

    private OrderItem(OrderId orderId, ProductId productId, int quantity, Money unitPrice)
    {
        Id = OrderItemId.New();
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static OrderItem TryCreate(OrderId orderId, ProductId productId, int quantity, Money unitPrice)
    {
        if (quantity <= 0)
        {
            throw new ValueBelowMinimumException("Quantity must be greater than zero.");
        }

        if (unitPrice.Value <= 0)
        {
            throw new ValueBelowMinimumException("Unit price must be greater than zero.");
        }

        return new OrderItem(orderId, productId, quantity, unitPrice);
    }

    public OrderId OrderId { get; private set; }
    public Product Product { get; private set; } = null!; // Navigation property
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    public Money TotalPrice => Money.From(Quantity * UnitPrice.Value);
}