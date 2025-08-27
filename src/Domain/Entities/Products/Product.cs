using Domain.Common.Interfaces;
using Domain.Exceptions;
using SharedKernel.Response;

namespace Domain.Entities.Products
{
    public sealed class Product : AggregateRoot<ProductId>, IAuditable, ISoftDeletable
    {
#pragma warning disable CS8618 
        private Product() { } // for EF Core
#pragma warning restore CS8618 
        
        private Product(ProductId id, string name, string description, decimal price, int quanity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quanity;
        }

        public static Product TryCreate(ProductId id, string name, string description, decimal price, int quanity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Product description cannot be empty.", nameof(description));
            }

            if (price <= 0)
            {
                throw new ValueBelowMinimumException("Price must be greater than zero.");
            }

            if (quanity < 0)
            {
                throw new ValueBelowMinimumException("Quantity cannot be negative.");
            }
            
            return new Product(id, name, description, price, quanity);
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        
        public Result TryReduceStock(int amount)
        {
            if (amount <= 0)
            {
                return Result.Failure(
                    ErrorFactory.Conflict("Amount to reduce must be greater than zero.")
                );
            }

            if (Quantity < amount)
            {
                return Result.Failure(
                    ErrorFactory.Conflict("Insufficient stock to reduce the requested amount.")
                );
            }

            Quantity -= amount;
            return Result.Success();
        }
        
        public Result TryIncreaseStock(int amount)
        {
            if (amount <= 0)
            {
                return Result.Failure(
                    ErrorFactory.Conflict("Amount to increase must be greater than zero.")
                );
            }

            Quantity += amount;
            return Result.Success();
        }

        public bool HasSufficientStock(int amount) => Quantity >= amount;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
