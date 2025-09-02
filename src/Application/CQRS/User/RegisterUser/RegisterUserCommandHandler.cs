using Application.Abstraction.Utils;
using Application.Mapper;
using Domain.Entities.Users;
using Domain.Entities.Users.DomainEvents;
using Domain.ValueObjects;

namespace Application.CQRS.User.RegisterUser;

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
            return Result.Failure(
                ErrorFactory.Conflict("The email is already registered")
            );
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