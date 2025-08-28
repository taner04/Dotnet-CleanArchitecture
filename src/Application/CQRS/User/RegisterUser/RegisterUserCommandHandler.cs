using Application.Abstraction.Utils;
using Application.DomainEvents.User.Event;
using Application.Mapper;
using Domain.Entities.Users;

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
        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email);
        if (existingUser is not null)
            return Result.Failure(
                ErrorFactory.Conflict("The email is already registered")
            );

        var newUser = command.ToUser(_passwordHasher.HashPassword(command.Password));

        var refreshToken = _tokenService.GenerateRefreshToken(newUser);
        newUser.SetJwt(new Jwt(refreshToken));

        newUser.AddDomainEvent(new UserRegisteredDomainEvent(newUser.FirstName, newUser.LastName, newUser.Email));

        _unitOfWork.UserRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}