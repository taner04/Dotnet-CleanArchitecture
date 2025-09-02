using Application.Abstraction.Utils;
using Domain.ValueObjects.Identifiers;

namespace Application.CQRS.Cart.RemoveCartItem;

public sealed class RemoveCartItemCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : ICommandHandler<RemoveCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

    public async ValueTask<Result> Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);
        if (cart == null)
        {
            return ErrorFactory.NotFound("Cart not found for the specified user.");
        }

        cart.RemoveCartItem(CartItemId.From(command.CartItemId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}