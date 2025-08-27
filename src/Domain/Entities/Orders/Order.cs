using Domain.Common.Interfaces;
using Domain.ValueObjects;
using SharedKernel.Enums;

namespace Domain.Entities.Orders;

/// <summary>
/// Represents an order aggregate root.
/// </summary>
public sealed class Order : AggregateRoot<OrderId>, IAuditable, ISoftDeletable
{
    private readonly List<OrderItem> _orderItems = [];

    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private Order()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="orderStatus">Initial order status.</param>
    private Order(OrderId id, UserId userId, OrderStatus orderStatus)
    {
        Id = id;
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        Status = orderStatus;
    }

    /// <summary>
    /// Adds an item to the order.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="quantity">Quantity of the product.</param>
    /// <param name="unitPrice">Unit price of the product.</param>
    public void AddOrderItem(ProductId productId, int quantity, Money unitPrice)
    {
        _orderItems.Add(OrderItem.TryCreate(Guid.CreateVersion7(), Id, productId, quantity, unitPrice));
    }

    /// <summary>
    /// Updates the status of the order.
    /// </summary>
    /// <param name="status">New order status.</param>
    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// Creates a new order instance if the provided IDs are valid.
    /// </summary>
    /// <param name="id">Order identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <returns>A new <see cref="Order"/> instance.</returns>
    /// <exception cref="InvalidIdException">Thrown when IDs are invalid.</exception>
    public static Order TryCreate(OrderId id, UserId userId)
    {
        if (id == Guid.Empty || userId == Guid.Empty) throw new InvalidIdException();

        return new Order(id, userId, OrderStatus.Pending);
    }

    /// <summary>
    /// Gets the collection of order items.
    /// </summary>
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    /// <summary>
    /// Gets the date and time the order was created.
    /// </summary>
    public DateTime OrderDate { get; private set; }

    /// <summary>
    /// Gets the user identifier associated with the order.
    /// </summary>
    public UserId UserId { get; private set; }

    /// <summary>
    /// Gets the current status of the order.
    /// </summary>
    public OrderStatus Status { get; private set; }

    /// <summary>
    /// Gets the total price of the order.
    /// </summary>
    public decimal TotalPrice => _orderItems.Sum(i => i.TotalPrice.Value);

    /// <summary>
    /// Gets or sets the creation date of the order.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated date of the order.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the order is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}