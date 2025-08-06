using SharedKernel.Enums;

namespace Domain.Entities
{
    public sealed class Order : Entity<OrderId>
    {
        public Order(decimal amount, PaymendMethod paymentMethod, string trackingNumber)
        {
            Amount = amount;
            OrderDate = DateTime.UtcNow;
            PaymentMethod = paymentMethod;
            OrderStatus = OrderStatus.Pending;
            TrackingNumber = trackingNumber;
        }

        public decimal Amount { get; init; }
        public DateTime OrderDate { get; init; }
        public PaymendMethod PaymentMethod { get; init; } 
        public OrderStatus OrderStatus { get; set; } 
        public string TrackingNumber { get; init; } 

        public UserId UserId { get; init; } 

        public List<OrderItem> OrderItems { get; init; } = null!;
    }
}
