namespace Domain.Entities
{
    public sealed class Order : Entity<OrderId>
    {
        public Order(decimal amount, DateTime orderDate, string paymentMethod)
        {
            Amount = amount;
            OrderDate = orderDate;
            PaymentMethod = paymentMethod;
        }

        public decimal Amount { get; init; }
        public DateTime OrderDate { get; init; }
        public string PaymentMethod { get; init; } = null!;
        public string OrderStatus { get; set; } = null!;

        public UserId UserId { get; init; } 

        public List<OrderItem> OrderItems { get; init; } = null!;
    }
}
