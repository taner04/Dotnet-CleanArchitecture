using Domain.ValueObjects;

namespace Domain.Entities.Orders
{
    public sealed class Order : AggregateRoot<OrderId>
    {
        private readonly List<OrderItem> _items = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _items.AsReadOnly();

        public DateTime OrderDate { get; private set; }

        private Order() { } // for EF

        public Order(OrderId id)
        {
            Id = id;
            OrderDate = DateTime.UtcNow;
        }

        public void AddItem(ProductId productId, int quantity, Money unitPrice)
        {
            var item = new OrderItem(OrderItemId.From(NewId()), productId, quantity, unitPrice);
            _items.Add(item);
        }

        public decimal TotalPrice => _items.Sum(i => i.TotalPrice.Value);
    }
}
