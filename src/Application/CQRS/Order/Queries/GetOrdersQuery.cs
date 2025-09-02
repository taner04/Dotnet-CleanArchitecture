using Application.Dtos.Order;
using Application.Mapper;

namespace Application.CQRS.Order.Queries;

public readonly record struct GetOrdersQuery : IQuery<ResultT<List<OrderDto>>>;

public sealed class GetOrdersQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IQueryHandler<GetOrdersQuery, ResultT<List<OrderDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    private readonly ICurrentUserService _currentUserService =
        currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

    public async ValueTask<ResultT<List<OrderDto>>> Handle(GetOrdersQuery command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        var orders = await _unitOfWork.OrderRepository.OrdersByUserAsync(userId);
        return orders.Select(o => o.ToOrderDto()).ToList();
    }
}