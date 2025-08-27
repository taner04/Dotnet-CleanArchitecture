using Application.Dtos.CartItem;
using Domain.Entities.Carts;

namespace Application.Mapper;

public static class CartItemMapper
{
    public static CartItemDto ToDto(this CartItem cartItem)
    {
        return new CartItemDto(cartItem.Id, cartItem.Quantity, cartItem.Product.ToProductCartDto());
    }
}