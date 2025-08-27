using Application.Dtos.Cart;
using Application.Mapper;

namespace Application.CQRS.Cart.GetCart;

public sealed class GetCartByUserQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetCartByUserQuery, ResultT<CartDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<CartDto>> Handle(GetCartByUserQuery query, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(query.UserId);
        if (cart != null) return ResultT<CartDto>.Success(cart.ToDto());

        cart = Domain.Entities.Carts.Cart.TryCreate(Guid.CreateVersion7(), query.UserId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultT<CartDto>.Success(cart.ToDto());
    }
}