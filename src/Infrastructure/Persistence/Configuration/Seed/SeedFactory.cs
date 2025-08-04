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
                    Id = new UserId(1),
                    CreatedAt = SeededDate,
                }
            );
        }

        private static void ProductSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product("Laptop", "A powerful laptop", 1499.99m, 20, "https://example.com/laptop.jpg")
                {
                    Id = new ProductId(1),
                    CreatedAt = SeededDate
                },
                new Product("Smartphone", "A modern smartphone", 899.99m, 50, "https://example.com/phone.jpg")
                {
                    Id = new ProductId(2),
                    CreatedAt = SeededDate
                }
            );
        }

        private static void OrderSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
               new Order(2399.98m, SeededDate, PaymendMethod.CreditCard)
               {
                   Id = new OrderId(1),
                   OrderStatus = OrderStatus.Pending,
                   UserId = new UserId(1),
                   CreatedAt = SeededDate
               }
           );
        }  
        
        private static void OrderItemSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem(1, 1499.99m)
                {
                    Id = new OrderItemId(1),
                    OrderId = new OrderId(1),
                    ProductId = new ProductId(1),
                    CreatedAt = SeededDate
                },
                new OrderItem(1, 899.99m)
                {
                    Id = new OrderItemId(2),
                    OrderId = new OrderId(1),
                    ProductId = new ProductId(2),
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
                    Id = new JwtId(1),
                    UserId = new UserId(1),
                    CreatedAt = SeededDate
                }
            );
        }
    }
}
