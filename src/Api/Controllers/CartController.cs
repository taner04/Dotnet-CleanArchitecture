using Application.CQRS.Cart.Commands;
using Application.CQRS.Cart.Queries;
using Application.Dtos.Cart;
using Application.Dtos.CartItem;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/cart")]
public sealed class CartController(IMediator mediator) : ControllerBase
{
    [HttpGet("user")]
    public async Task<IActionResult> GetCartByUserAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCartByUserQuery(), cancellationToken);
        return MapResponse(result);
    }

    [HttpPost("item/add")]
    public async Task<IActionResult> AddItemToCartAsync([FromBody] AddCartItemCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return MapResponse(result);
    }

    [HttpDelete("item/remove")]
    public async Task<IActionResult> RemoveItemFromCartAsync([FromBody] RemoveCartItemCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return MapResponse(result);
    }
}