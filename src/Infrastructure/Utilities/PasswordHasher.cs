using Application.Common.Interfaces.Infrastructure;

namespace Infrastructure.Utilities
{
    [ServiceInjection(typeof(IPasswordHasher), ScopeType.AddTransient)]
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password) 
            => BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));

        public bool VerifyPassword(string password, string hashedPassword) 
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
