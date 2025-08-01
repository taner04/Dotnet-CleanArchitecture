namespace Infrastructure.Persistence.Configuration.Seed
{
    public static class OrderItemSeed
    {
        public static OrderItem OrderItem1 => new(5, ProductSeed.Product1.Price * 5)
        {
            Order = OrderSeed.Order,
            OrderId = OrderSeed.Order.Id,
            Product = ProductSeed.Product1,
            ProductId = ProductSeed.Product1.Id
        };

        public static OrderItem OrderItem2 => new(10, ProductSeed.Product1.Price * 10)
        {
            Order = OrderSeed.Order,
            OrderId = OrderSeed.Order.Id,
            Product = ProductSeed.Product2,
            ProductId = ProductSeed.Product2.Id
        };

        public static List<OrderItem> OrderItems =>
        [
            OrderItem1,
            OrderItem2
        ];
    }
}
