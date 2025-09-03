using Domain.Entities.Base;
using Domain.Entities.Users;
using Domain.ValueObjects;
using SharedKernel.Response;
using CartId = Domain.ValueObjects.Identifiers.CartId;
using CartItemId = Domain.ValueObjects.Identifiers.CartItemId;
using ProductId = Domain.ValueObjects.Identifiers.ProductId;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Domain.Entities.Carts;

public sealed class Cart : AggregateRoot<CartId>
{
    private readonly List<CartItem> _cartItems = [];

#pragma warning disable CS8618
    private Cart()
    {
    } // for EFC
#pragma warning restore CS8618

    private Cart(UserId userId)
    {
        Id = CartId.New();
        UserId = userId;
    }

    public static Cart TryCreate(UserId userId)
    {
        return new Cart(userId);
    }

    public void AddCartItem(ProductId productId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ValueBelowMinimumException("Add amount must be greater than zero.");
        }

        var existingCartItem = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingCartItem != null)
        {
            existingCartItem.IncrementQuantity(quantity);
        }
        else
        {
            _cartItems.Add(CartItem.TryCreate(Id, productId, quantity));
        }
    }

    public void RemoveCartItem(CartItemId cartItemId)
    {
        var cartItem = _cartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        if (cartItem == null)
        {
            throw new DomainException($"Cart item with ID {cartItemId} not found in cart.");
        }

        _cartItems.Remove(cartItem);
    }

    public UserId UserId { get; private set; }
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public User User { get; set; } = null!; // Navigation property
}