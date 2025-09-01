using Application.CQRS.User.RegisterUser;
using Application.Dtos.User;
using Domain.Entities.Users;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.Mapper;

public static class UserMapper
{
    public static AuthResponse ToAuthResponse(this User user, string accessToken)
    {
        return new AuthResponse(
            user.Id.Value,
            user.FirstName,
            user.LastName,
            user.Email,
            accessToken,
            user.RefreshToken.Value
        );
    }

    public static User ToUser(this RegisterUserCommand request)
    {
        return User.TryCreate(
            UserId.From(Guid.CreateVersion7()),
            request.FirstName,
            request.LastName,
            request.Email
        );
    }
}