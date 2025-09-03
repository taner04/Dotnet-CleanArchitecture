using Application.Abstraction;
using Application.Mapper;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Users;
using Domain.Entities.Users.DomainEvents;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands;

public readonly record struct RegisterUserCommand(string FirstName, string LastName, string Email, string Password)
    : ICommand<Result>;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IApplicationDbContext dbContext,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async ValueTask<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var email = Email.From(command.Email);
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
        if (existingUser is not null)
        {
            return ErrorFactory.Conflict("The email is already registered");
        }

        var newUser = command.ToUser();

        newUser.SetRefreshToken(_tokenService.GenerateRefreshToken(newUser));
        newUser.SetPasswordHash(_passwordHasher.HashPassword(command.Password));

        newUser.AddDomainEvent(new UserRegisteredDomainEvent(newUser.FirstName, newUser.LastName, newUser.Email.Value));

        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Firstname is required.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Lastname is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}