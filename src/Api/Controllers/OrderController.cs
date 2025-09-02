using Application.CQRS.Order.CancelOrder;
using Application.CQRS.Order.CreateOrder;
using Application.CQRS.Order.GetOrders;
using Application.Dtos.Order;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetOrdersByUserAsync()
    {
        var result = await _mediator.Send(new GetOrdersQuery());
        return MapResponse(result);
    }

    [HttpPost("user/new-order")]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return MapResponse(result);
    }

    [HttpDelete("user/cancel")]
    public async Task<IActionResult> CancelOrderAsync([FromBody] CancelOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return MapResponse(result);
    }
}