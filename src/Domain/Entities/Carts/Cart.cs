using Domain.Entities.Base;
using Domain.Entities.Users;
using Domain.ValueObjects;
using SharedKernel.Response;

namespace Domain.Entities.Carts;

public sealed class Cart : AggregateRoot<CartId>
{
    private readonly List<CartItem> _cartItems = [];

#pragma warning disable CS8618
    private Cart()
    {
    } // for EF Core
#pragma warning restore CS8618

    private Cart(CartId id, UserId userId)
    {
        Id = id;
        UserId = userId;
    }

    public static Cart TryCreate(CartId id, UserId userId)
    {
        if (id == Guid.Empty || userId == Guid.Empty) throw new InvalidIdException();

        return new Cart(id, userId);
    }

    public void AddCartItem(ProductId productId, int quantity)
    {
        if (quantity <= 0) throw new ValueBelowMinimumException("Add amount must be greater than zero.");

        var existingCartItem = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingCartItem != null)
            existingCartItem.IncrementQuantity(quantity);
        else
            _cartItems.Add(CartItem.TryCreate(Guid.CreateVersion7(), Id, productId, quantity));
    }

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

    public UserId UserId { get; set; }
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}