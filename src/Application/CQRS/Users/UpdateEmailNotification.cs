using Application.Common;
using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using Shared.Errors;

namespace Application.CQRS.Users;

public static class UpdateEmailNotification
{
    public record Command(bool WantsEmailNotifications) : ICommand<ErrorOr<Success>>;

    internal sealed class Handler(
        IApplicationDbContext dbContext,
        UserService userService) : ICommandHandler<Command, ErrorOr<Success>>
    {
        public async ValueTask<ErrorOr<Success>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await userService.GetCurrentUserAsync(cancellationToken);

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