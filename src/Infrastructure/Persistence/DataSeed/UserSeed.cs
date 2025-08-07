using Domain.Entities.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataSeed
{
    public static class UserSeed
    {
        public static UserId UserId => UserId.From(Guid.Parse("00000000-0000-0000-0000-000000000001"));
        public static string Password => "$2a$12$Ct3JJ.gjadRRkxoAn3iu7.xmubwh/YogC/QyEwB9k8Hf5BYb8mmZ6";
        public static Jwt Jwt => new(
            JwtToken.From(""),
            JwtToken.From("")
        );

        public static void Seed(ModelBuilder modelBuilder)
        {
            var user = new User(UserId, "John", "Doe", "doe@mail.com", Password);

            user.SetJwt(Jwt);
            modelBuilder.Entity<User>().HasData(user);
        }
    }
}
