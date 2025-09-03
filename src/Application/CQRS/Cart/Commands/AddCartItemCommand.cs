using Application.Abstraction;
using Application.Validator;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Cart.Commands;

public readonly record struct AddCartItemCommand(Guid ProductId, int Quantity) : ICommand<Result>;

public sealed class AddCartItemCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : ICommandHandler<AddCartItemCommand, Result>
{
    public async ValueTask<Result> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();
        var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        if (cart == null)
        {
            cart = Domain.Entities.Carts.Cart.TryCreate(userId);
            dbContext.Carts.Add(cart);
        }

        var productId = ProductId.From(command.ProductId);
        if (await dbContext.Products.FindAsync(productId, cancellationToken) == null)
        {
            return ErrorFactory.NotFound("Product not found.");
        }

        cart.AddCartItem(productId, command.Quantity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public sealed class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.ProductId)
            .IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}