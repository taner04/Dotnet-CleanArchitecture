using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataSeed
{
    public static class ProductSeed
    {
        public static ProductId ProductId => ProductId.From(Guid.Parse("00000000-0000-0000-0000-000000000001"));

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product(ProductId, "Apple iPhone 15", "The latest Apple smartphone with A17 chip and 128GB storage.", 999.99m)
            );
        }
    }
}
