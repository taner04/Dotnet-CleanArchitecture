using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using SharedKernel.Errors;

namespace Application.CQRS.Users;

public static class UpdateEmailNotification
{
    public record Command(bool WantsEmailNotifications) : ICommand<ErrorOr<Success>>;

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
                return UserErrors.Unauthorized;
            }

            user.ChangeEmailNotificationPreference(command.WantsEmailNotifications);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.WantsEmailNotifications).NotEmpty().NotNull();
        }
    }
}