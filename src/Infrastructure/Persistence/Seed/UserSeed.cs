using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed
{
    public static class UserSeed
    {
        public static void Seed(DbContext dbContext)
        {
            var dummyUser = new User("John", "Doe", "doe@mail.com", "John123!");

            dbContext.Set<User>().RemoveRange(dbContext.Set<User>().ToList());
            dbContext.Set<User>().Add(dummyUser);
        }
    }
}
