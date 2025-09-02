using Domain.Entities.Base;
using Domain.Entities.Products;
using Domain.ValueObjects;
using CartId = Domain.ValueObjects.Identifiers.CartId;
using CartItemId = Domain.ValueObjects.Identifiers.CartItemId;
using ProductId = Domain.ValueObjects.Identifiers.ProductId;

namespace Domain.Entities.Carts;

public sealed class CartItem : Entity<CartItemId>
{
#pragma warning disable CS8618
    private CartItem()
    {
    } // for EFC
#pragma warning restore CS8618

    private CartItem(CartId cartId, ProductId productId, int quantity)
    {
        Id = CartItemId.New();
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public static CartItem TryCreate(CartId cartId, ProductId productId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ValueBelowMinimumException("Quantity must be greater than zero.");
        }

        return new CartItem(cartId, productId, quantity);
    }

    public void IncrementQuantity(int amount)
    {
        if (amount <= 0)
        {
            throw new ValueBelowMinimumException("Increment amount must be greater than zero.");
        }

        Quantity += amount;
    }

    public CartId CartId { get; private set; }
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Product Product { get; set; } = null!; // Navigation property
}