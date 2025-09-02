using Application.Mapper;
using Domain.Entities.Users.DomainEvents;
using Domain.ValueObjects;

namespace Application.CQRS.User.Commands;

public readonly record struct RegisterUserCommand(string FirstName, string LastName, string Email, string Password)
    : ICommand<Result>;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async ValueTask<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(Email.From(command.Email));
        if (existingUser is not null)
        {
            return ErrorFactory.Conflict("The email is already registered");
        }

        var newUser = command.ToUser();

        newUser.SetRefreshToken(_tokenService.GenerateRefreshToken(newUser));
        newUser.SetPasswordHash(_passwordHasher.HashPassword(command.Password));

        newUser.AddDomainEvent(new UserRegisteredDomainEvent(newUser.FirstName, newUser.LastName, newUser.Email.Value));

        _unitOfWork.UserRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Firstname is required.")
            .MaximumLength(50)
            .WithMessage("Firstname must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Lastname is required.")
            .MaximumLength(50)
            .WithMessage("Lastname must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.")
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(256)
            .WithMessage("Password must not exceed 256 characters.");
    }
}