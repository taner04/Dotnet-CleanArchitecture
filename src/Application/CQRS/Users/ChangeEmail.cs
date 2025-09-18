using Application.Abstraction.Infrastructure;
using Domain.Entities.Users.ValueObjects;

namespace Application.CQRS.Users;

public static class ChangeEmail
{
    public record Command(string NewEmail) : ICommand<ErrorOr<Success>>;
    
    internal sealed class Handler(
        IBudgetDbContext dbContext,
        ICurrentUserService currentUserService) : ICommandHandler<Command, ErrorOr<Success>>
    {
        public async ValueTask<ErrorOr<Success>> Handle(Command command, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            
            if (user is null)
            {
                return Error.NotFound("User not found");
            }
            
            user.ChangeEmail(command.NewEmail);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return Result.Success;
        }
    }
    
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.NewEmail).NotEmpty().NotNull().EmailAddress();
        }
    }
}