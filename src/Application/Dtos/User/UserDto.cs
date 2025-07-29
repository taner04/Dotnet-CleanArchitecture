using Domain.Entities.TypedIds;

namespace Application.Dtos.User
{
    public record UserDto(UserId Id, string Firstname, string Lastname, string Mail);
}
