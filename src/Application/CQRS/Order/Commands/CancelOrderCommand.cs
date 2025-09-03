using Application.Abstraction;
using Application.Validator;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Order.Commands;

public readonly record struct CancelOrderCommand(Guid OrderId) : ICommand<Result>;

public sealed class CancelOrderCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : ICommandHandler<CancelOrderCommand, Result>
{
    public async ValueTask<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.From(command.OrderId);
        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        if (order is null)
        {
            return ErrorFactory.NotFound($"Order with ID {command.OrderId} not found.");
        }

        if (order.UserId != currentUserService.GetUserId())
        {
            return ErrorFactory.Unauthorized("You cannot cancel an order that does not belong to you.");
        }

        order.Cancel();

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public sealed class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderValidator()
    {
        RuleFor(x => x.OrderId).IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}