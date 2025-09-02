using Application.CQRS.Cart.AddCartItem;
using Application.CQRS.Cart.GetCart;
using Application.CQRS.Cart.RemoveCartItem;
using Application.Dtos.Cart;
using Application.Dtos.CartItem;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/cart")]
public sealed class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetCartByUserAsync()
    {
        var result = await _mediator.Send(new GetCartByUserQuery());
        return MapResponse(result);
    }

    [HttpPost("item/add")]
    public async Task<IActionResult> AddItemToCartAsync([FromBody] AddCartItemCommand command)
    {
        var result = await _mediator.Send(command);
        return MapResponse(result);
    }

    [HttpDelete("item/remove")]
    public async Task<IActionResult> RemoveItemFromCartAsync([FromBody] RemoveCartItemCommand command)
    {
        var result = await _mediator.Send(command);
        return MapResponse(result);
    }
}