using Application.Abstraction;
using Application.Dtos.Cart;
using Application.Mapper;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Cart.Queries;

public readonly record struct GetCartByUserQuery : IQuery<ResultT<CartDto>>;

public sealed class GetCartByUserQueryHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : IQueryHandler<GetCartByUserQuery, ResultT<CartDto>>
{
    public async ValueTask<ResultT<CartDto>> Handle(GetCartByUserQuery query, CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();

        var cart = await dbContext.Carts
            .WithSpecification(new CartByUserSpecification(userId))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (cart != null)
        {
            return cart.ToDto();
        }

        cart = Domain.Entities.Carts.Cart.TryCreate(userId);
        await dbContext.SaveChangesAsync(cancellationToken);

        return cart.ToDto();
    }
}