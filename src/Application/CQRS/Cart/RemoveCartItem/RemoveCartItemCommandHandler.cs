using Application.Abstraction.Utils;

namespace Application.CQRS.Cart.RemoveCartItem;

public sealed class RemoveCartItemCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<Result> Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(command.UserId);
        if (cart == null)
            return Result.Failure(
                ErrorFactory.NotFound("Cart not found for the specified user.")
            );

        var result = cart.TryRemoveCartItem(command.CartItemId);
        if (!result.IsSuccess) return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}