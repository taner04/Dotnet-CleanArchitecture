using Application.Dtos.Cart;
using Application.Dtos.CartItem;
using SharedKernel.Response;

namespace Application.Common.Interfaces.Services;

public interface ICartService
{
    Task<ResultT<CartDto>> GetCartByUserId(CartByUserDto cartByUser);
    Task<Result> AddItemToCart(AddCartItemDto addCartItem);
    Task<Result> RemoveItemFromCart(RemoveCartItemDto removeCartItem);
}