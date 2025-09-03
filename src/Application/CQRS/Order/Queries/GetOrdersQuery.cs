using Application.Abstraction;
using Application.Dtos.Order;
using Application.Mapper;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Order.Queries;

public readonly record struct GetOrdersQuery : IQuery<ResultT<List<OrderDto>>>;

public sealed class GetOrdersQueryHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : IQueryHandler<GetOrdersQuery, ResultT<List<OrderDto>>>
{
    public async ValueTask<ResultT<List<OrderDto>>> Handle(GetOrdersQuery command, CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();

        var orders = await dbContext.Orders
            .WithSpecification(new OrdersByUserSpecification(userId))
            .ToListAsync(cancellationToken);
        
        return orders.Select(o => o.ToOrderDto()).ToList();
    }
}