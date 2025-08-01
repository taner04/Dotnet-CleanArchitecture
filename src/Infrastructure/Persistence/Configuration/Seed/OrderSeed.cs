namespace Infrastructure.Persistence.Configuration.Seed
{
    public static class OrderSeed
    {
        public static Order Order => new(100, DateTime.UtcNow, PaymendMethod.PayPal)
        {
            Id = new OrderId(1),
            Amount = 5000,
            OrderDate = DateTime.UtcNow,
            PaymentMethod = PaymendMethod.PayPal,
            OrderStatus = OrderStatus.Pending,

            UserId = UserSeed.User.Id,
            OrderItems =
            [
                OrderItemSeed.OrderItem1,
                OrderItemSeed.OrderItem2
            ],
        };
    }
}
