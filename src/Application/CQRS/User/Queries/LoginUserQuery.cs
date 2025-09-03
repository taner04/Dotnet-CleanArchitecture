using Application.Abstraction;
using Application.Dtos.User;
using Application.Mapper;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Queries;

public readonly record struct LoginUserQuery(string Email, string Password) : IQuery<ResultT<AuthResponse>>;

public class LoginUserQueryHandler(
    IApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    ITokenService tokenService)
    : IQueryHandler<LoginUserQuery, ResultT<AuthResponse>>
{
    public async ValueTask<ResultT<AuthResponse>> Handle(LoginUserQuery query, CancellationToken cancellationToken)
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

        if (!existingUser.HasValidRefreshToken)
        {
            var refreshToken = tokenService.GenerateRefreshToken(existingUser);
            existingUser.SetRefreshToken(refreshToken);

            dbContext.Users.Update(existingUser);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        var accessToken = tokenService.GenerateAccessToken(existingUser);
        return existingUser.ToAuthResponse(accessToken);
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