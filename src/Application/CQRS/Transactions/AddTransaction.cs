using Application.Abstraction.Infrastructure;

namespace Application.CQRS.Transactions;

public static class AddTransaction
{
    public record Command(decimal Amount, TransactionType Type, string Description) : ICommand<ErrorOr<Success>>;
    
    internal sealed class Handler(
        IBudgetDbContext dbContext,
        ICurrentUserService currentUserService) : ICommandHandler<Command, ErrorOr<Success>>
    {
        public async ValueTask<ErrorOr<Success>> Handle(Command command, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            
            var user = await dbContext.Users.Where(x => x.Id == userId)
                                            .Include(x => x.Account)
                                            .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return UserErrors.Unauthorized;
            }
            
            var newTransaction = Transaction.TryCreate(command.Amount, command.Type, command.Description);
            user.AddTransaction(newTransaction);
            
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
    
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Description).MaximumLength(250).NotEmpty().NotNull();
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}