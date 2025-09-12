using Application.Abstraction.Persistence;
using Domain.Entities.Users;

namespace Infrastructure;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(User user, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword , user.PasswordHash.Value);
    }
}