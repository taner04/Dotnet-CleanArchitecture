using Application.Dtos.User;

namespace Application.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(User user)
        {
            return new(
                user.Id.Value,
                user.Firstname,
                user.Lastname,
                user.Email,
                user.Jwt!.Token
            );
        }
    }
}
