using Application.Abstraction;
using Application.Dtos.Product;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Order.Commands;

public readonly record struct CreateOrderCommand(List<ProductOrderCreateDto> Products) : ICommand<Result>;

public sealed class CreateOrderCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : ICommandHandler<CreateOrderCommand, Result>
{
    public async ValueTask<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user is null)
        {
            return ErrorFactory.NotFound($"User with ID {userId.Value} not found.");
        }

        var order = Domain.Entities.Orders.Order.TryCreate(userId);
        foreach (var product in command.Products)
        {
            var productId = ProductId.From(product.ProductId);
            var productEntity = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            if (productEntity is null)
            {
                return ErrorFactory.NotFound($"Product with ID {productId} not found.");
            }

            productEntity.ReduceStock(product.Quantity);
            dbContext.Products.Update(productEntity);

            order.AddOrderItem(productId, product.Quantity, productEntity.Price);
        }

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(order => order.Products)
            .NotEmpty()
            .WithMessage("Order items cannot be empty.")
            .Must(items => items.Count > 0)
            .WithMessage("At least one order item is required.");
    }
}