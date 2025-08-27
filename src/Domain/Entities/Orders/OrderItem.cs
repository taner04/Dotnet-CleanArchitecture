using Domain.Entities.Products;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.Orders;

/// <summary>
/// Represents an item within an order, including product, quantity, and unit price.
/// </summary>
public sealed class OrderItem : Entity<OrderItemId>
{
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private OrderItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItem"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the order item.</param>
    /// <param name="orderId">The identifier of the associated order.</param>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="quantity">The quantity of the product ordered.</param>
    /// <param name="unitPrice">The price per unit of the product.</param>
    private OrderItem(OrderItemId id, OrderId orderId, ProductId productId, int quantity, Money unitPrice)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    /// <summary>
    /// Attempts to create a new <see cref="OrderItem"/> instance, validating input parameters.
    /// </summary>
    /// <param name="id">The unique identifier for the order item.</param>
    /// <param name="orderId">The identifier of the associated order.</param>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="quantity">The quantity of the product ordered.</param>
    /// <param name="unitPrice">The price per unit of the product.</param>
    /// <returns>A valid <see cref="OrderItem"/> instance.</returns>
    /// <exception cref="InvalidIdException">Thrown when any ID is invalid.</exception>
    /// <exception cref="ValueBelowMinimumException">Thrown when quantity or unit price is below minimum.</exception>
    public static OrderItem TryCreate(OrderItemId id, OrderId orderId, ProductId productId, int quantity,
        Money unitPrice)
    {
        if (id == Guid.Empty || productId == Guid.Empty || orderId == Guid.Empty) throw new InvalidIdException();

        if (quantity <= 0) throw new ValueBelowMinimumException("Quantity must be greater than zero.");

        if (unitPrice.Value <= 0) throw new ValueBelowMinimumException("Unit price must be greater than zero.");

        return new OrderItem(id, orderId, productId, quantity, unitPrice);
    }

    /// <summary>
    /// Gets the identifier of the associated order.
    /// </summary>
    public OrderId OrderId { get; private set; }

    /// <summary>
    /// Gets the associated product entity. Navigation property.
    /// </summary>
    public Product Product { get; private set; } = null!; // Navigation property

    /// <summary>
    /// Gets the identifier of the product.
    /// </summary>
    public ProductId ProductId { get; private set; }

    /// <summary>
    /// Gets the quantity of the product ordered.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the price per unit of the product.
    /// </summary>
    public Money UnitPrice { get; private set; }

    /// <summary>
    /// Gets the total price for this order item (quantity * unit price).
    /// </summary>
    public Money TotalPrice => Money.From(Quantity * UnitPrice.Value);
}