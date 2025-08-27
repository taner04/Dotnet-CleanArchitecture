using Application.Dtos.Cart;
using Application.Dtos.CartItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/cart")]
public sealed class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("user")]
    public async Task<IActionResult> GetCartByUserAsync([FromBody] CartByUserDto cartByUserDto)
    {
        var result = await _cartService.GetCartByUserId(cartByUserDto);
        return MapResponse(result);
    }

    [HttpPost("item/add")]
    public async Task<IActionResult> AddItemToCartAsync([FromBody] AddCartItemDto addCartItemDto)
    {
        var result = await _cartService.AddItemToCart(addCartItemDto);
        return MapResponse(result);
    }

    [HttpDelete("item/remove")]
    public async Task<IActionResult> RemoveItemFromCartAsync([FromBody] RemoveCartItemDto removeCartItem)
    {
        var result = await _cartService.RemoveItemFromCart(removeCartItem);
        return MapResponse(result);
    }
}