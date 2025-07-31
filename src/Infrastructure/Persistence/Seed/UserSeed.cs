using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed
{
    public static class UserSeed
    {
        public static void Seed(DbContext dbContext)
        {
            var user = new User("John", "Doe", "doe@mail.com", "$2a$12$Eju2KmPviy2UCJIUAlTtr.LFZ/DbdsFOOlN3YEoP5p30HLxwe1YXG")
            {
                Jwt = new(
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzUzODkyNjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.LJf4HNEJE8KLwnfBaVcO-MbJI_vXNAg_ZDdfkIwUoZ4",
                    DateTime.UtcNow,
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzU0NDkwMjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.6byhPpfHXqbF2lDjOUgWoQ8v8O45Bnbh_R8W0pib2oA",
                    DateTime.UtcNow)
            };

            dbContext.Set<User>().Add(user);
        }
    }
}
