using Application.Abstraction.Utils;
using Application.Dtos.Cart;
using Application.Mapper;
using Domain.ValueObjects.Identifiers;

namespace Application.CQRS.Cart.GetCart;

public sealed class GetCartByUserQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetCartByUserQuery, ResultT<CartDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<CartDto>> Handle(GetCartByUserQuery query, CancellationToken cancellationToken)
    {
        var userId = UserId.From(query.UserId);
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);
        if (cart != null) return ResultT<CartDto>.Success(cart.ToDto());

        cart = Domain.Entities.Carts.Cart.TryCreate(userId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultT<CartDto>.Success(cart.ToDto());
    }
}