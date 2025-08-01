using SharedKernel.Enums;

namespace Domain.Entities
{
    public sealed class Order : Entity<OrderId>
    {
        public Order(decimal amount, DateTime orderDate, PaymendMethod paymentMethod)
        {
            Amount = amount;
            OrderDate = orderDate;
            PaymentMethod = paymentMethod;
        }

        public decimal Amount { get; init; }
        public DateTime OrderDate { get; init; }
        public PaymendMethod PaymentMethod { get; init; } 
        public OrderStatus OrderStatus { get; set; } 

        public UserId UserId { get; init; } 

        public List<OrderItem> OrderItems { get; init; } = null!;
    }
}
