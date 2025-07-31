namespace Domain.Entities
{
    public sealed class Product : Entity<ProductId>
    {
        public Product(string name, string description, decimal price, int stock, string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImageUrl = imageUrl;
        }

        public string Name { get; init; } = null!; 
        public string Description { get; init; } = null!;
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public string ImageUrl { get; init; } = null!;
    }
}
