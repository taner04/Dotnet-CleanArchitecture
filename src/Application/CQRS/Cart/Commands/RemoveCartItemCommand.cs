using Application.Validator;

namespace Application.CQRS.Cart.Commands;

public readonly record struct RemoveCartItemCommand(Guid CartItemId) : ICommand<Result>;

public sealed class RemoveCartItemCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : ICommandHandler<RemoveCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    private readonly ICurrentUserService _currentUserService =
        currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

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

public sealed class RemoveCartItemValidator : AbstractValidator<RemoveCartItemCommand>
{
    public RemoveCartItemValidator()
    {
        RuleFor(x => x.CartItemId)
            .IsId()
            .WithMessage("Cart item id is invalid");
    }
}