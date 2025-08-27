using Domain.Common.Interfaces;
using Domain.Entities.Users;
using Domain.ValueObjects;
using SharedKernel.Response;
using SharedKernel.Response.Errors;
using SharedKernel.Response.Results;

namespace Domain.Entities.Carts;

/// <summary>
/// Represents a shopping cart aggregate root.
/// </summary>
public sealed class Cart : AggregateRoot<CartId>
{
    private readonly List<CartItem> _cartItems = [];

    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private Cart()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cart"/> class with the specified ID and user ID.
    /// </summary>
    /// <param name="id">The cart ID.</param>
    /// <param name="userId">The user ID.</param>
    private Cart(CartId id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }

    /// <summary>
    /// Attempts to create a new <see cref="Cart"/> instance.
    /// Throws <see cref="InvalidIdException"/> if IDs are invalid.
    /// </summary>
    /// <param name="id">The cart ID.</param>
    /// <param name="userId">The user ID.</param>
    /// <returns>A new <see cref="Cart"/> instance.</returns>
    public static Cart TryCreate(CartId id, UserId userId)
    {
        if (id == Guid.Empty || userId == Guid.Empty) throw new InvalidIdException();

        return new Cart(id, userId);
    }

    /// <summary>
    /// Adds a cart item with the specified product ID and quantity.
    /// Increments quantity if the item already exists.
    /// Throws <see cref="ValueBelowMinimumException"/> if quantity is not positive.
    /// </summary>
    /// <param name="productId">The product ID.</param>
    /// <param name="quantity">The quantity to add.</param>
    public void AddCartItem(ProductId productId, int quantity)
    {
        if (quantity <= 0) throw new ValueBelowMinimumException("Add amount must be greater than zero.");

        var existingCartItem = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingCartItem != null)
            existingCartItem.IncrementQuantity(quantity);
        else
            _cartItems.Add(CartItem.TryCreate(Guid.CreateVersion7(), Id, productId, quantity));
    }

    /// <summary>
    /// Attempts to remove a cart item by its ID.
    /// Returns a failure result if the item is not found.
    /// </summary>
    /// <param name="cartItemId">The cart item ID.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    public Result TryRemoveCartItem(CartItemId cartItemId)
    {
        var cartItem = _cartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        if (cartItem == null)
            return Result.Failure(
                ErrorFactory.NotFound($"Cart item with ID {cartItemId} not found in cart.")
            );

        _cartItems.Remove(cartItem);
        return Result.Success();
    }

    /// <summary>
    /// Gets or sets the user ID associated with the cart.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets the read-only collection of cart items.
    /// </summary>
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    /// <summary>
    /// Gets or sets the creation date and time of the cart.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated date and time of the cart.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}