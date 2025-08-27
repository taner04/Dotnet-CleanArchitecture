using Domain.Common.Interfaces;
using Domain.Entities.Products;
using Domain.ValueObjects;

namespace Domain.Entities.Carts;

public sealed class CartItem : Entity<CartItemId>
{
#pragma warning disable CS8618 
    private CartItem() { } // for EF Core
#pragma warning restore CS8618

    private CartItem(CartItemId id, CartId cartId, ProductId productId, int quantity)
    {
        Id = id;
        CartId = cartId;
        ProductId = productId;  
        Quantity = quantity;
    }
    
    public static CartItem TryCreate(CartItemId id, CartId cartId, ProductId productId, int quantity)
    {
        if (id == Guid.Empty ||  cartId == Guid.Empty || productId == Guid.Empty)
        {
            throw new InvalidIdException();
        }
        
        if (quantity <= 0)
        {
            throw new ValueBelowMinimumException("Quantity must be greater than zero.");
        }
        
        return new CartItem(id, cartId, productId, quantity);
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
    
    public bool IsDeleted { get; set; } = false;
}