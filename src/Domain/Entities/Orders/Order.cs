using Domain.ValueObjects;

namespace Domain.Entities.Orders
{
    public sealed class Order : AggregateRoot<OrderId>
    {
        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public DateTime OrderDate { get; private set; }
        public UserId UserId { get; private set; }

        private Order() { } // for EF

        public Order(OrderId id, UserId userId)
        {
            Id = id;
            OrderDate = DateTime.UtcNow;
            UserId = userId;
        }

        public void AddOrderItem(ProductId productId, int quantity, Money unitPrice)
        {
            var item = new OrderItem(OrderItemId.From(NewId()), productId, quantity, unitPrice);
            _orderItems.Add(item);
        }

        public decimal TotalPrice => _orderItems.Sum(i => i.TotalPrice.Value);
    }
}
