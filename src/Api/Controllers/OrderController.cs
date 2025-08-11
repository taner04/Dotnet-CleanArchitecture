using Application.Dtos.Order;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpPost("user")]
        public async Task<IActionResult> GetOrdersByUserAsync([FromBody] OrderByUserDto orderByUserDto)
        {
            var result = await _orderService.GetOrdersByUserAsync(UserId.From(orderByUserDto.UserId));
            return MapResponse(result);
        }

        [HttpPost("user/new-order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderCreateDto orderCreateDto)
        {
            var result = await _orderService.CreateOrderAsync(orderCreateDto);
            return MapResponse(result);
        }

        [HttpDelete("user/cancel")]
        public async Task<IActionResult> CancelOrderAsync([FromBody] OrderCancelDto orderCancelDto)
        {
            var result = await _orderService.CancelOrderAsync(orderCancelDto);
            return MapResponse(result);
        }
    }
}
