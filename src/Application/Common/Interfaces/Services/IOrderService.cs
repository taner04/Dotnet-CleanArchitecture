using Application.Dtos.Order;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Result<List<OrderByIdDto>>> GetOrdersByUser(UserId userId);
        Task<Result<bool>> CancelOrderAsync(OrderCancelDto orderCancel);
        Task<Result<bool>> CreateOrderAsync(OrderCreateDto order);
    }
}
