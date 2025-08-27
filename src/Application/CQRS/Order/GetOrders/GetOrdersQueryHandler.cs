using Application.Dtos.Order;
using Application.Mapper;

namespace Application.CQRS.Order.GetOrders;

public sealed class GetOrdersQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetOrdersQuery, ResultT<List<OrderDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<List<OrderDto>>> Handle(GetOrdersQuery command, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.OrderRepository.OrdersByUserAsync(command.UserId);
        return ResultT<List<OrderDto>>.Success([.. orders.Select(o => o.ToOrderDto())]);
    }
}