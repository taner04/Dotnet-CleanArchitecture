using Application.Dtos.User;
using Domain.Entities.Users;

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
            user.Jwt.RefreshToken.Value
        );
    }

    public static User ToUser(this RegisterRequest request, string hashedPwd)
    {
        return User.TryCreate(
            UserId.From(Guid.CreateVersion7()),
            request.FirstName,
            request.LastName,
            request.Email,
            hashedPwd
        );
    }
}