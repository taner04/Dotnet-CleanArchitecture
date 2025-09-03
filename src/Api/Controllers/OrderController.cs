using Application.CQRS.Order.Commands;
using Application.CQRS.Order.Queries;
using Application.Dtos.Order;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/orders")]
public class OrderController(IMediator mediator) : ControllerBase
{
    [HttpGet("user")]
    public async Task<IActionResult> GetOrdersByUserAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrdersQuery(), cancellationToken);
        return MapResponse(result);
    }

    [HttpPost("user/new-order")]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return MapResponse(result);
    }

    [HttpDelete("user/cancel")]
    public async Task<IActionResult> CancelOrderAsync([FromBody] CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return MapResponse(result);
    }
}