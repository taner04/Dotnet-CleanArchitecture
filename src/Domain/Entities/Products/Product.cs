using Domain.Abstraction;
using Domain.Entities.Base;
using Domain.Exceptions.Product;
using Domain.ValueObjects;
using Domain.ValueObjects.Identifiers;

namespace Domain.Entities.Products;

public sealed class Product : AggregateRoot<ProductId>, ISoftDeletable
{
#pragma warning disable CS8618
    private Product()
    {
    } // for EFC
#pragma warning restore CS8618

    private Product(string name, string description, decimal price, int quantity)
    {
        Id = ProductId.New();
        Name = name;
        Description = description;
        Price = Money.From(price);
        Quantity = quantity;
    }

    public static Product TryCreate(string name, string description, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Product name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException("Product description cannot be empty.");
        }

        if (price <= 0)
        {
            throw new ValueBelowMinimumException("Price must be greater than zero.");
        }

        if (quantity < 0)
        {
            throw new ValueBelowMinimumException("Quantity cannot be negative.");
        }

        return new Product(name, description, price, quantity);
    }

    public void ReduceStock(int amount)
    {
        if (amount <= 0)
        {
            throw new UpdateStockException("Amount to reduce must be greater than zero");
        }

        if (Quantity < amount)
        {
            throw new UpdateStockException("Insufficient stock to reduce the requested amount.");
        }

        Quantity -= amount;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new UpdateStockException("Quantity to increase must be greater than zero.");
        }

        Quantity += quantity;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }
    public bool IsDeleted { get; set; }
}