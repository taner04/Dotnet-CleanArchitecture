using Application.Dtos.Order;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ResultT<List<OrderDto>>> GetOrdersByUserAsync(UserId userId);
        Task<Result> CreateOrderAsync(OrderCreateDto orderCreate);
        Task<Result> CancelOrderAsync(OrderCancelDto orderCancelDto);
    }
}
