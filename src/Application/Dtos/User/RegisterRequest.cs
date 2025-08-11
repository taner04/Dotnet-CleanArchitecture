namespace Application.Dtos.User
{
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password);
}
