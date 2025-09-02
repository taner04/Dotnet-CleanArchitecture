using Domain.Abstraction;
using Domain.Entities.Base;
using Domain.Entities.Orders.DomainEvents;
using Domain.Entities.Users;
using Domain.Exceptions.Order;
using Domain.ValueObjects;
using Domain.ValueObjects.Identifiers;
using SharedKernel.Enums;

namespace Domain.Entities.Orders;

public sealed class Order : AggregateRoot<OrderId>, ISoftDeletable
{
    private readonly List<OrderItem> _orderItems = [];


#pragma warning disable CS8618
    private Order()
    {
    } // for EFC
#pragma warning restore CS8618

    private Order(UserId userId, OrderStatus orderStatus)
    {
        Id = OrderId.New();
        ;
        UserId = userId;
        OrderDate = DateTime.UtcNow;
        Status = orderStatus;

        AddDomainEvent(new OrderConfirmationDomainEvent(userId, this));
    }

    public void AddOrderItem(ProductId productId, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new OrderModificationException("Cannot add items to an order that is not pending.");
        }

        _orderItems.Add(OrderItem.TryCreate(Id, productId, quantity, unitPrice));
    }

    public void Cancel()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new OrderCancelException();
        }

        AddDomainEvent(new OrderCancelledDomainEvent(
            _orderItems.ToDictionary(e => e.ProductId, e => e.Quantity)));

        Status = OrderStatus.Cancelled;
    }

    public static Order TryCreate(UserId userId)
    {
        return new Order(userId, OrderStatus.Pending);
    }


    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTime OrderDate { get; private set; }
    public UserId UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Money TotalPrice => Money.From(_orderItems.Sum(i => i.TotalPrice.Value));
    public bool IsDeleted { get; set; }

    public User User { get; private set; } = null!; // Navigation property
}