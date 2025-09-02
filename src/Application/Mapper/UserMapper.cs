using Application.CQRS.User.RegisterUser;
using Application.Dtos.User;
using Domain.Entities.Users;

namespace Application.Mapper;

public static class UserMapper
{
    public static AuthResponse ToAuthResponse(this User user, string accessToken)
    {
        return new AuthResponse(
            user.FirstName,
            user.LastName,
            user.Email.Value,
            accessToken,
            user.RefreshToken.Value
        );
    }

    public static User ToUser(this RegisterUserCommand request)
    {
        return User.TryCreate(
            request.FirstName,
            request.LastName,
            request.Email
        );
    }
}