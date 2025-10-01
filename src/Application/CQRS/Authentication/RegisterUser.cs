using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.ApplicationUsers.ValueObjects;
using Shared.Errors;
namespace Application.CQRS.Authentication;

public static class RegisterUser
{
    public record Command(string FirstName, string LastName, string Email, string Password, bool WantsEmailNotification)
        : ICommand<ErrorOr<Success>>;

    internal sealed class Handler(
        IApplicationDbContext applicationDbContext,
        IPasswordService passwordService) : ICommandHandler<Command, ErrorOr<Success>>
    {
        public async ValueTask<ErrorOr<Success>> Handle(Command command, CancellationToken cancellationToken)
        {
            var emailResult = Email.TryFrom(command.Email);
            if (!emailResult.IsSuccess)
            {
                return UserErrors.InvalidEmail;
            }

            var mail = emailResult.ValueObject;
            if (await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == mail, cancellationToken) != null)
            {
                return UserErrors.AlreadyExists;
            }

            var newUser = ApplicationUser.TryCreate(command.FirstName, command.LastName, mail, command.WantsEmailNotification);

            var hashedPassword = passwordService.HashPassword(command.Password);
            newUser.SetPassword(Password.From(hashedPassword));

            await applicationDbContext.Users.AddAsync(newUser, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Password).NotEmpty().MinimumLength(8);
            RuleFor(u => u.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(u => u.LastName).NotEmpty().MaximumLength(50);
            RuleFor(u => u.WantsEmailNotification).NotNull();
        }
    }
}