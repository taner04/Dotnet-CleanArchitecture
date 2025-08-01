using Application.Dtos.Order;
using Domain.Entities.TypedIds;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/orders")]
    public sealed class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllOrdersAsync([FromRoute] UserId userId)
        {
            var result = await _orderService.GetOrdersByUser(userId);
            return MapResponse(result);
        }

        [HttpPost("cancel/{orderCancel.UserId}/order/{orderCancel.OrderId}")]
        public async Task<IActionResult> CancelOrderAsync([FromBody] OrderCancelDto orderCancel)
        {
            var result = await _orderService.CancelOrderAsync(orderCancel);    
            return MapResponse(result);
        }
    }
}
