using Domain.Entities.Users.ValueObjects;

namespace Application.Abstraction.Persistence;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(User user, string providedPassword);
}