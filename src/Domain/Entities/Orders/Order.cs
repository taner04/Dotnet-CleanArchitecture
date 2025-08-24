using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.Orders
{
    public sealed class Order : AggregateRoot<OrderId>
    {
        private readonly List<OrderItem> _orderItems = [];

        private Order() { } // for EF

        private Order(OrderId id, UserId userId)
        {
            Id = id;
            UserId = userId;
            OrderDate = DateTime.UtcNow;
        }

        public void AddOrderItem(ProductId productId, int quantity, Money unitPrice)
        {
            _orderItems.Add(OrderItem.TryCreate(Guid.CreateVersion7(), productId, quantity, unitPrice));
        }

        public static Order TryCreate(OrderId id, UserId userId)
        {
            if (id == Guid.Empty || userId == Guid.Empty)
            {
                throw new InvalidIdException();
            }

            return new Order(id, userId);
        }
        
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public DateTime OrderDate { get; private set; }
        public UserId UserId { get; private set; }
        public decimal TotalPrice => _orderItems.Sum(i => i.TotalPrice.Value);
    }
}
