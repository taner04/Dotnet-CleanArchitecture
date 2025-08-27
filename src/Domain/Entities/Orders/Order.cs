using Domain.Common.Interfaces;
using Domain.ValueObjects;
using SharedKernel.Enums;

namespace Domain.Entities.Orders;

public sealed class Order : AggregateRoot<OrderId>, IAuditable, ISoftDeletable
{
    private readonly List<OrderItem> _orderItems = [];

    private Order()
    {
    } // for EF

    private Order(OrderId id, UserId userId, OrderStatus orderStatus)
    {
        Id = id;
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        Status = orderStatus;
    }

    public void AddOrderItem(ProductId productId, int quantity, Money unitPrice)
    {
        _orderItems.Add(OrderItem.TryCreate(Guid.CreateVersion7(), Id, productId, quantity, unitPrice));
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    public static Order TryCreate(OrderId id, UserId userId)
    {
        if (id == Guid.Empty || userId == Guid.Empty) throw new InvalidIdException();

        return new Order(id, userId, OrderStatus.Pending);
    }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTime OrderDate { get; private set; }
    public UserId UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalPrice => _orderItems.Sum(i => i.TotalPrice.Value);
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}