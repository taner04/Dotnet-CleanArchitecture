using Application.Common.Abstraction.Infrastructure;
using Domain.Entities.ApplicationUsers;

namespace Infrastructure.Utils;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(ApplicationUser applicationUser, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, applicationUser.PasswordHash.Value);
    }
}