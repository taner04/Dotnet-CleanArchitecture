using Domain.Common.Interfaces;
using Domain.Exceptions;
using SharedKernel.Response;
using SharedKernel.Response.Errors;
using SharedKernel.Response.Results;

namespace Domain.Entities.Products;

/// <summary>
/// Represents a product entity with stock management and auditing capabilities.
/// </summary>
public sealed class Product : AggregateRoot<ProductId>, IAuditable, ISoftDeletable
{
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private Product()
    {
    }

    /// <summary>
    /// Private constructor to initialize a product with specified values.
    /// </summary>
    /// <param name="id">The unique product identifier.</param>
    /// <param name="name">The product name.</param>
    /// <param name="description">The product description.</param>
    /// <param name="price">The product price.</param>
    /// <param name="quanity">The product quantity in stock.</param>
    private Product(ProductId id, string name, string description, decimal price, int quanity)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quanity;
    }

    /// <summary>
    /// Attempts to create a new product instance, validating input parameters.
    /// </summary>
    /// <param name="id">The unique product identifier.</param>
    /// <param name="name">The product name.</param>
    /// <param name="description">The product description.</param>
    /// <param name="price">The product price.</param>
    /// <param name="quanity">The product quantity in stock.</param>
    /// <returns>A new <see cref="Product"/> instance if validation passes.</returns>
    /// <exception cref="ArgumentException">Thrown if name or description is invalid.</exception>
    /// <exception cref="ValueBelowMinimumException">Thrown if price or quantity is invalid.</exception>
    public static Product TryCreate(ProductId id, string name, string description, decimal price, int quanity)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Product name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description cannot be empty.", nameof(description));

        if (price <= 0) throw new ValueBelowMinimumException("Price must be greater than zero.");

        if (quanity < 0) throw new ValueBelowMinimumException("Quantity cannot be negative.");

        return new Product(id, name, description, price, quanity);
    }

    /// <summary>
    /// Updates the product details.
    /// </summary>
    /// <param name="name">The new product name.</param>
    /// <param name="description">The new product description.</param>
    /// <param name="price">The new product price.</param>
    public void UpdateDetails(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    /// <summary>
    /// Attempts to reduce the product stock by a specified amount.
    /// </summary>
    /// <param name="amount">The amount to reduce.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    public Result TryReduceStock(int amount)
    {
        if (amount <= 0)
            return Result.Failure(
                ErrorFactory.Conflict("Amount to reduce must be greater than zero.")
            );

        if (Quantity < amount)
            return Result.Failure(
                ErrorFactory.Conflict("Insufficient stock to reduce the requested amount.")
            );

        Quantity -= amount;
        return Result.Success();
    }

    /// <summary>
    /// Attempts to increase the product stock by a specified amount.
    /// </summary>
    /// <param name="amount">The amount to increase.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    public Result TryIncreaseStock(int amount)
    {
        if (amount <= 0)
            return Result.Failure(
                ErrorFactory.Conflict("Amount to increase must be greater than zero.")
            );

        Quantity += amount;
        return Result.Success();
    }

    /// <summary>
    /// Checks if the product has sufficient stock for a given amount.
    /// </summary>
    /// <param name="amount">The required amount.</param>
    /// <returns>True if sufficient stock exists; otherwise, false.</returns>
    public bool HasSufficientStock(int amount)
    {
        return Quantity >= amount;
    }

    /// <summary>
    /// Gets the product name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the product description.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the product price.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Gets the product quantity in stock.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets or sets the creation date of the product.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date of the product.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}