using Application.Abstraction;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Queries;

public readonly record struct LoginUserQuery(string Email, string Password) : IQuery<ResultT<string>>;

public class LoginUserQueryHandler(
    IApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    ITokenService tokenService)
    : IQueryHandler<LoginUserQuery, ResultT<string>>
{
    public async ValueTask<ResultT<string>> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var email = Email.From(query.Email);
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
        if (existingUser is null)
        {
            return ErrorFactory.NotFound("User not found");
        }

        if (!passwordHasher.VerifyPassword(query.Password, existingUser.Password.Value))
        {
            return ErrorFactory.Unauthorized("Invalid password");
        }

        if (existingUser.HasValidRefreshToken)
        {
            return tokenService.GenerateAccessToken(existingUser);
        }

        var refreshToken = tokenService.GenerateRefreshToken(existingUser);
        existingUser.SetRefreshToken(refreshToken);

        dbContext.Users.Update(existingUser);
        await dbContext.SaveChangesAsync(cancellationToken);

        return tokenService.GenerateAccessToken(existingUser);
    }
}

public sealed class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}