namespace Application.Dtos.User
{
    public record AuthResponse(Guid Id, string Firstname, string Lastname, string Mail, string Token, string RefreshToken);
}
