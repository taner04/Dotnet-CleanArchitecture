namespace Application.Common.Interfaces.Infrastructure
{
    public interface ITokenGenerator
    {
        Jwt GenerateToken(string email, UserId userId);
    }
}
