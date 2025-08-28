using Domain.Entities.Products;
using Domain.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seeds;

public static class ProductSeed
{
    public static void SeedProducts(this ModelBuilder modelBuilder)
    {
        var products = new List<Product>
        {
            Product.TryCreate(ProductId.From(Guid.Parse("e807f68c-e98c-7348-9b4e-8a24a62f0e15")),
                "Apple MacBook Air M2", "Lightweight laptop with Apple's M2 chip", 1199.99m, 10),
            Product.TryCreate(ProductId.From(Guid.Parse("0cb16e4a-467c-7dcd-86ff-542d3ca37d8b")), "Samsung Galaxy S23",
                "Flagship Android smartphone", 949.00m, 15),
            Product.TryCreate(ProductId.From(Guid.Parse("97449bb7-8e90-72fc-a5af-177f62554331")),
                "Logitech MX Master 3S", "Advanced wireless mouse", 99.99m, 20),
            Product.TryCreate(ProductId.From(Guid.Parse("bcf74560-1f6c-7962-aa83-8c1cf0e90fef")), "Sony WH-1000XM5",
                "Noise-cancelling over-ear headphones", 349.99m, 5),
            Product.TryCreate(ProductId.From(Guid.Parse("9d10c1bb-b729-74b9-919e-5996eaa4feab")),
                "Amazon Echo Dot (5th Gen)", "Smart speaker with Alexa", 49.99m, 30),
            Product.TryCreate(ProductId.From(Guid.Parse("4f2b3e8f-b137-7962-abce-cae1c62489d5")),
                "Dell UltraSharp U2723QE", "27-inch 4K monitor", 679.99m, 8),
            Product.TryCreate(ProductId.From(Guid.Parse("125daedf-2ed9-7fc0-be1c-ab4125fc0dbe")),
                "Anker PowerCore 20100", "Portable charger 20100mAh", 49.95m, 25),
            Product.TryCreate(ProductId.From(Guid.Parse("ee1bda8b-ab9d-79ae-972b-5d598e3d4ed1")), "Canon EOS R6",
                "Full-frame mirrorless camera", 2499.00m, 12),
            Product.TryCreate(ProductId.From(Guid.Parse("84fa579c-057b-7bf2-b85d-1aeb218bf2b4")),
                "Nintendo Switch OLED", "Handheld gaming console with OLED screen", 349.99m, 18),
            Product.TryCreate(ProductId.From(Guid.Parse("51336668-7448-7081-b4e8-7ff9c92d6731")),
                "Kindle Paperwhite Signature", "E-reader with 32GB storage and warm light", 189.99m, 14)
        };
        modelBuilder.Entity<Product>().HasData(products);
    }
}