namespace Application.Dtos.User
{
    public record UserDto(Guid Id, string Firstname, string Lastname, string Mail, string Token);
}
