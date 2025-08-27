using Domain.Common.Interfaces;
using Domain.Entities.Products;
using Domain.ValueObjects;

namespace Domain.Entities.Carts;

/// <summary>
/// Represents an item in a shopping cart.
/// </summary>
public sealed class CartItem : Entity<CartItemId>
{
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private CartItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CartItem"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the cart item.</param>
    /// <param name="cartId">The identifier of the cart this item belongs to.</param>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="quantity">The quantity of the product.</param>
    private CartItem(CartItemId id, CartId cartId, ProductId productId, int quantity)
    {
        Id = id;
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    /// <summary>
    /// Attempts to create a new <see cref="CartItem"/> instance with validation.
    /// </summary>
    /// <param name="id">The unique identifier for the cart item.</param>
    /// <param name="cartId">The identifier of the cart.</param>
    /// <param name="productId">The identifier of the product.</param>
    /// <param name="quantity">The quantity of the product.</param>
    /// <returns>A new <see cref="CartItem"/> instance.</returns>
    /// <exception cref="InvalidIdException">Thrown when any ID is invalid.</exception>
    /// <exception cref="ValueBelowMinimumException">Thrown when quantity is less than or equal to zero.</exception>
    public static CartItem TryCreate(CartItemId id, CartId cartId, ProductId productId, int quantity)
    {
        if (id == Guid.Empty || cartId == Guid.Empty || productId == Guid.Empty) throw new InvalidIdException();

        if (quantity <= 0) throw new ValueBelowMinimumException("Quantity must be greater than zero.");

        return new CartItem(id, cartId, productId, quantity);
    }

    /// <summary>
    /// Increments the quantity of the cart item.
    /// </summary>
    /// <param name="amount">The amount to increment by.</param>
    /// <exception cref="ValueBelowMinimumException">Thrown when amount is less than or equal to zero.</exception>
    public void IncrementQuantity(int amount)
    {
        if (amount <= 0) throw new ValueBelowMinimumException("Increment amount must be greater than zero.");

        Quantity += amount;
    }

    /// <summary>
    /// Gets the identifier of the cart this item belongs to.
    /// </summary>
    public CartId CartId { get; private set; }

    /// <summary>
    /// Gets the identifier of the product.
    /// </summary>
    public ProductId ProductId { get; private set; }

    /// <summary>
    /// Gets the quantity of the product.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets or sets the product associated with this cart item.
    /// </summary>
    public Product Product { get; set; } = null!; // Navigation property
}