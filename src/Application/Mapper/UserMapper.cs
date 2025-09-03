using Application.CQRS.User.Commands;
using Application.Dtos.User;
using Domain.Entities.Users;

namespace Application.Mapper;

public static class UserMapper
{
    public static UserInfoResponse ToUserInfoResponse(this User user)
    {
        return new UserInfoResponse(
            user.FirstName,
            user.LastName,
            user.Email.Value
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