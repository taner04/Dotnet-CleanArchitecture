namespace Infrastructure.Persistence.Configuration.Seed
{
    public static class ProductSeed
    {
        public static Product Product1 = new("Sample Product 1", "Description for sample product 1", 19.99m, 100, "https://example.com/image1.jpg")
        {
            Id = new ProductId(1) 
        };
        
        public static Product Product2 = new("Sample Product 2", "Description for sample product 2", 29.99m, 50, "https://example.com/image2.jpg")
        {
            Id = new ProductId(2)
        };

        public static Product Product3 = new("Sample Product 3", "Description for sample product 3", 39.99m, 25, "https://example.com/image3.jpg")
        {
            Id = new ProductId(3)
        };

        public static Product Product4 = new("Sample Product 4", "Description for sample product 4", 49.99m, 10, "https://example.com/image4.jpg")
        {
            Id = new ProductId(4)
        };
        
        public static Product Product5 = new("Sample Product 5", "Description for sample product 5", 59.99m, 5, "https://example.com/image5.jpg")
        {
            Id = new ProductId(5)
        };

        public static List<Product> Products =>
        [
            Product1,
            Product2,
            Product3,
            Product4,
            Product5
        ];
    }
}
