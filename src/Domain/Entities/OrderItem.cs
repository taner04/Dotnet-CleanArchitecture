namespace Domain.Entities
{
    public sealed class OrderItem : Entity<OrderItemId>
    {
        public OrderItem(int quantity, decimal price)
        {
            Quantity = quantity;
            Price = price;
        }

        public int Quantity { get; init; }
        public decimal Price { get; init; }

        public OrderId OrderId { get; init; }
        public Order Order { get; init; } = null!;

        public ProductId ProductId { get; init; }
        public Product Product { get; init; } = null!;
    }
}
