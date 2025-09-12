using Application.Abstraction.Infrastructure;
using Application.Abstraction.Persistence;
using Domain.Entities.Users.ValueObjects;
using ErrorOr;
using FluentValidation;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users;

public static class LoginUser
{
    public record Query(string Email, string Password) : IQuery<ErrorOr<string>>;
    
    internal class QueryHandler(
        IBudgetDbContext budgetDbContext, 
        IPasswordService passwordService,
        ITokenService<Domain.Entities.Users.User> tokenService) : IQueryHandler<Query, ErrorOr<string>>
    {
        public async ValueTask<ErrorOr<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            var email = Email.From(query.Email);
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