using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using Domain.Entities.Users.DomainEvents;
using Domain.Entities.Users.ValueObjects;
using SharedKernel.Errors;

namespace Application.CQRS.Authentication;

public static class LoginUser
{
    public record Query(string Email, string Password) : IQuery<ErrorOr<string>>;

    internal sealed class Handler(
        IBudgetDbContext budgetDbContext,
        IPasswordService passwordService,
        ITokenService<User> tokenService) : IQueryHandler<Query, ErrorOr<string>>
    {
        public async ValueTask<ErrorOr<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            var emailResult = Email.TryFrom(query.Email);
            if (!emailResult.IsSuccess)
            {
                return UserErrors.InvalidEmail;
            }

            var email = emailResult.ValueObject;
            var user = await budgetDbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (user is null || !passwordService.VerifyPassword(user, query.Password))
            {
                return UserErrors.InvalidCredentials;
            }

            if (!user.IsRefreshTokenValid)
            {
                user.SetRefreshToken(tokenService.GenerateRefreshToken(user));
                await budgetDbContext.SaveChangesAsync(cancellationToken);
            }

            user.AddDomainEvent(new UserLoggedInDomainEvent(user));

            return tokenService.GenerateAccessToken(user);
        }
    }

    internal sealed class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
        }
    }
}