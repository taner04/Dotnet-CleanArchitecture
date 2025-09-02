using Application.Abstraction.Utils;
using Application.Dtos.Order;
using Application.Mapper;
using Domain.ValueObjects.Identifiers;

namespace Application.CQRS.Order.GetOrders;

public sealed class GetOrdersQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IQueryHandler<GetOrdersQuery, ResultT<List<OrderDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    
    public async ValueTask<ResultT<List<OrderDto>>> Handle(GetOrdersQuery command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        
        var orders = await _unitOfWork.OrderRepository.OrdersByUserAsync(userId);
        return ResultT<List<OrderDto>>.Success([.. orders.Select(o => o.ToOrderDto())]);
    }
}