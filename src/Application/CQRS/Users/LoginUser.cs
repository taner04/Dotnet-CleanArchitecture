using Application.Abstraction.Infrastructure;
using Domain.Entities.Users.ValueObjects;

namespace Application.CQRS.Users;

public static class LoginUser
{
    public record Query(string Email, string Password) : IQuery<ErrorOr<string>>;
    
    internal class Handler(
        IBudgetDbContext budgetDbContext, 
        IPasswordService passwordService,
        ITokenService<Domain.Entities.Users.User> tokenService) : IQueryHandler<Query, ErrorOr<string>>
    {
        public async ValueTask<ErrorOr<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            var emailResult = Email.TryFrom(query.Email);
            if (!emailResult.IsSuccess)
            {
                return Error.Validation(description: "Invalid email format");
            }
            
            var email = emailResult.ValueObject;
            var user = await budgetDbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            
            if (user is null || !passwordService.VerifyPassword(user, query.Password))
            {
                return Error.Unauthorized(description: "Invalid credentials");
            }

            if (user.IsRefreshTokenValid)
            {
                return tokenService.GenerateAccessToken(user);
            }

            var refreshToken = tokenService.GenerateRefreshToken(user);
            user.SetRefreshToken(refreshToken);
            
            await budgetDbContext.SaveChangesAsync(cancellationToken);

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