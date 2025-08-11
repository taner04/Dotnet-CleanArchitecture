using Application.Dtos.Order;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ResultT<List<OrderDto>>> GetOrdersByUserAsync(UserId userId);
        Task<Result> CreateOrderAsync(CreateOrderRequest orderCreate);
        Task<Result> CancelOrderAsync(CancelOrderRequest orderCancelDto);
    }
}
