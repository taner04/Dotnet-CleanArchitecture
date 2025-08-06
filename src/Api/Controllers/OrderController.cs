using Application.Dtos.Order;
using Domain.Entities;
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

        [HttpPost("user")]
        public async Task<IActionResult> GetAllOrdersFromUserAsync([FromBody] OrdersByUserIdDto ordersByUserIdDto)
        {
            var result = await _orderService.GetOrdersByUser(UserId.From(ordersByUserIdDto.Id));
            return MapResponse(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderCreateDto order)
        {
            var result = await _orderService.CreateOrderAsync(order);
            return MapResponse(result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelOrderAsync([FromBody] OrderCancelDto orderCancel)
        {
            var result = await _orderService.CancelOrderAsync(orderCancel);   
            return MapResponse(result);
        }
    }
}
