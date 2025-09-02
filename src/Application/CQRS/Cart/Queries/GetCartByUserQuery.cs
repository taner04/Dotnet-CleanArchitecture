using Application.Dtos.Cart;
using Application.Mapper;

namespace Application.CQRS.Cart.Queries;

public readonly record struct GetCartByUserQuery : IQuery<ResultT<CartDto>>;

public sealed class GetCartByUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IQueryHandler<GetCartByUserQuery, ResultT<CartDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    private readonly ICurrentUserService _currentUserService =
        currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

    public async ValueTask<ResultT<CartDto>> Handle(GetCartByUserQuery query, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);
        if (cart != null)
        {
            return cart.ToDto();
        }

        cart = Domain.Entities.Carts.Cart.TryCreate(userId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return cart.ToDto();
    }
}