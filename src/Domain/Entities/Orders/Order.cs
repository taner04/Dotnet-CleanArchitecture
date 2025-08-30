using Domain.Abstraction;
using Domain.Entities.Base;
using Domain.ValueObjects;
using SharedKernel.Enums;

namespace Domain.Entities.Orders;

public sealed class Order : AggregateRoot<OrderId>, ISoftDeletable
{
    private readonly List<OrderItem> _orderItems = [];

    
#pragma warning disable CS8618
    private Order() { } // for EFC
#pragma warning restore CS8618

    private Order(UserId userId, OrderStatus orderStatus)
    {
        Id = Guid.CreateVersion7();
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        Status = orderStatus;
    }

    public void AddOrderItem(ProductId productId, int quantity, Money unitPrice)
    {
        _orderItems.Add(OrderItem.TryCreate(Id, productId, quantity, unitPrice));
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    public static Order TryCreate(UserId userId) => new (userId, OrderStatus.Pending);
    

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTime OrderDate { get; private set; }
    public UserId UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalPrice => _orderItems.Sum(i => i.TotalPrice.Value);
    public bool IsDeleted { get; set; }
}