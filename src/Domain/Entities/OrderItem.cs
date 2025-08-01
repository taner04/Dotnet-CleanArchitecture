namespace Domain.Entities
{
    public sealed class OrderItem : Entity<OrderItemId>
    {
        public OrderItem(int quantity, decimal amount)
        {
            Quantity = quantity;
            Amount = amount;
        }

        public int Quantity { get; init; }
        public decimal Amount { get; init; }

        public OrderId OrderId { get; init; }
        public Order Order { get; init; } = null!;

        public ProductId ProductId { get; init; }
        public Product Product { get; init; } = null!;
    }
}
