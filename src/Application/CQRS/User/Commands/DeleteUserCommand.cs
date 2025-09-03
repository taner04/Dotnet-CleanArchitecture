using Application.Abstraction;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;

namespace Application.CQRS.User.Commands;

public readonly record struct DeleteUserCommand : ICommand<Result>;

public sealed class DeleteUserCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : ICommandHandler<DeleteUserCommand, Result>
{
    public async ValueTask<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();
        
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (user is null)
        {
            return ErrorFactory.NotFound($"User with ID {userId.Value} not found.");
        }

        var orders = await dbContext.Orders.Where(o => o.UserId == userId).ToListAsync(cancellationToken);
        if (orders.Any(o => o.Status == OrderStatus.Pending))
        {
            return ErrorFactory.Conflict("User cannot be deleted while having pending orders.");
        }

        dbContext.Orders.RemoveRange(orders);

        var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        if (cart != null)
        {
            dbContext.Carts.Remove(cart);
        }

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}