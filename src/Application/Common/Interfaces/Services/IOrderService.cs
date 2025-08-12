using Application.Dtos.Order;
using SharedKernel.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ResultT<List<OrderDto>>> GetOrdersByUserAsync(OrderByUserDto orderByUser);
        Task<Result> CreateOrderAsync(CreateOrderRequest orderCreate);
        Task<Result> CancelOrderAsync(CancelOrderRequest orderCancelDto);
    }
}
