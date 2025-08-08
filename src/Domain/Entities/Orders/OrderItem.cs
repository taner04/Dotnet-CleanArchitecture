using Domain.Entities.Products;
using Domain.ValueObjects;

namespace Domain.Entities.Orders
{
    public sealed class OrderItem : Entity<OrderItemId>
    {
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        public Money TotalPrice => Money.From(Quantity * UnitPrice.Value);

#pragma warning disable CS8618
        private OrderItem() { } // for EF
#pragma warning restore CS8618 

        public OrderItem(OrderItemId id, ProductId productId, int quantity, Money unitPrice)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Product Product { get; private set; } = null!; // EF Core will set this property
    }
}
