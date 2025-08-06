using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configuration.Seed
{
    public static class SeedFactory
    {
        private static readonly DateTime SeededDate = new(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            UserSeed(modelBuilder);
            ProductSeed(modelBuilder);
            OrderSeed(modelBuilder);
            OrderItemSeed(modelBuilder);
            JwtSeed(modelBuilder);
        }

        private static void UserSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User("John", "Doe", "doe@mail.com", "$2a$12$Eju2KmPviy2UCJIUAlTtr.LFZ/DbdsFOOlN3YEoP5p30HLxwe1YXG")
                {
                    Id = UserId.From(1),
                    CreatedAt = SeededDate,
                }
            );
        }

        private static void ProductSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product("Laptop", "A powerful laptop", 1499.99m, 20, "https://example.com/laptop.jpg")
                {
                    Id = ProductId.From(1),
                    CreatedAt = SeededDate
                },
                new Product("Smartphone", "A modern smartphone", 899.99m, 50, "https://example.com/phone.jpg")
                {
                    Id = ProductId.From(2),
                    CreatedAt = SeededDate
                },
                new Product("Tablet", "A lightweight tablet", 499.99m, 35, "https://example.com/tablet.jpg")
                {
                    Id = ProductId.From(3),
                    CreatedAt = SeededDate
                },
                new Product("Monitor", "27-inch 4K monitor", 329.99m, 15, "https://example.com/monitor.jpg")
                {
                    Id = ProductId.From(4),
                    CreatedAt = SeededDate
                },
                new Product("Keyboard", "Mechanical keyboard", 89.99m, 60, "https://example.com/keyboard.jpg")
                {
                    Id = ProductId.From(5),
                    CreatedAt = SeededDate
                },
                new Product("Mouse", "Wireless mouse", 49.99m, 80, "https://example.com/mouse.jpg")
                {
                    Id = ProductId.From(6),
                    CreatedAt = SeededDate
                },
                new Product("Headphones", "Noise-cancelling headphones", 199.99m, 40, "https://example.com/headphones.jpg")
                {
                    Id = ProductId.From(7),
                    CreatedAt = SeededDate
                },
                new Product("Webcam", "HD webcam", 69.99m, 30, "https://example.com/webcam.jpg")
                {
                    Id = ProductId.From(8),
                    CreatedAt = SeededDate
                },
                new Product("Printer", "Laser printer", 159.99m, 10, "https://example.com/printer.jpg")
                {
                    Id = ProductId.From(9),
                    CreatedAt = SeededDate
                },
                new Product("Router", "Wi-Fi 6 router", 129.99m, 25, "https://example.com/router.jpg")
                {
                    Id = ProductId.From(10),
                    CreatedAt = SeededDate
                },
                new Product("External SSD", "1TB portable SSD", 119.99m, 45, "https://example.com/ssd.jpg")
                {
                    Id = ProductId.From(11),
                    CreatedAt = SeededDate
                },
                new Product("Smartwatch", "Fitness smartwatch", 249.99m, 22, "https://example.com/smartwatch.jpg")
                {
                    Id = ProductId.From(12),
                    CreatedAt = SeededDate
                },
                new Product("Speakers", "Bluetooth speakers", 79.99m, 55, "https://example.com/speakers.jpg")
                {
                    Id = ProductId.From(13),
                    CreatedAt = SeededDate
                },
                new Product("Gaming Chair", "Ergonomic gaming chair", 299.99m, 12, "https://example.com/chair.jpg")
                {
                    Id = ProductId.From(14),
                    CreatedAt = SeededDate
                },
                new Product("Microphone", "USB condenser microphone", 109.99m, 18, "https://example.com/microphone.jpg")
                {
                    Id = ProductId.From(15),
                    CreatedAt = SeededDate
                },
                new Product("Graphics Card", "High-end graphics card", 799.99m, 8, "https://example.com/gpu.jpg")
                {
                    Id = ProductId.From(16),
                    CreatedAt = SeededDate
                },
                new Product("Power Bank", "20000mAh power bank", 39.99m, 70, "https://example.com/powerbank.jpg")
                {
                    Id = ProductId.From(17),
                    CreatedAt = SeededDate
                },
                new Product("VR Headset", "Virtual reality headset", 399.99m, 6, "https://example.com/vr.jpg")
                {
                    Id = ProductId.From(18),
                    CreatedAt = SeededDate
                },
                new Product("Smart Home Hub", "Voice-controlled smart hub", 99.99m, 28, "https://example.com/hub.jpg")
                {
                    Id = ProductId.From(19),
                    CreatedAt = SeededDate
                },
                new Product("Drone", "Camera drone", 599.99m, 5, "https://example.com/drone.jpg")
                {
                    Id = ProductId.From(20),
                    CreatedAt = SeededDate
                }
            );
        }

        private static void OrderSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
               new Order(2399.98m, PaymendMethod.CreditCard, "CD587DB4-C345-4D86-9D9C-F9C0A0BF50D3")
               {
                   Id = OrderId.From(1),
                   OrderStatus = OrderStatus.Pending,
                   UserId = UserId.From(1),
                   CreatedAt = SeededDate,
                   OrderDate = SeededDate,
               }
           );
        }  
        
        private static void OrderItemSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem(1, 1499.99m)
                {
                    Id = OrderItemId.From(1),
                    OrderId = OrderId.From(1),
                    ProductId = ProductId.From(1),
                    CreatedAt = SeededDate
                },
                new OrderItem(1, 899.99m)
                {
                    Id = OrderItemId.From(2),
                    OrderId = OrderId.From(1),
                    ProductId = ProductId.From(2),
                    CreatedAt = SeededDate
                }
            );
        }

        private static void JwtSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jwt>().HasData(
                new Jwt(
                         "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzUzODkyNjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.LJf4HNEJE8KLwnfBaVcO-MbJI_vXNAg_ZDdfkIwUoZ4",
                         SeededDate,
                         "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzU0NDkwMjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.6byhPpfHXqbF2lDjOUgWoQ8v8O45Bnbh_R8W0pib2oA",
                         SeededDate
                    )
                {
                    Id = JwtId.From(1),
                    UserId = UserId.From(1),
                    CreatedAt = SeededDate
                }
            );
        }
    }
}
