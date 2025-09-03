using Application.Abstraction;
using Application.Validator;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Cart.Commands;

public readonly record struct RemoveCartItemCommand(Guid CartItemId) : ICommand<Result>;

public sealed class RemoveCartItemCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : ICommandHandler<RemoveCartItemCommand, Result>
{
    public async ValueTask<Result> Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();

        var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        if (cart == null)
        {
            return ErrorFactory.NotFound("Cart not found for the specified user.");
        }

        cart.RemoveCartItem(CartItemId.From(command.CartItemId));

        await dbContext.SaveChangesAsync(cancellationToken);

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