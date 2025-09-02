using Application.Dtos.Cart;
using Domain.Entities.Carts;

namespace Application.Mapper;

public static class CartMapper
{
    public static CartDto ToDto(this Cart cart)
    {
        return new CartDto(cart.Id.Value, cart.CartItems.Select(ci => ci.ToDto()).ToList());
    }
}