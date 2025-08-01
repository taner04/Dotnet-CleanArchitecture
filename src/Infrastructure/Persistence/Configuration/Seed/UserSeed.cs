namespace Infrastructure.Persistence.Configuration.Seed
{
    public static class UserSeed
    {
        public static User User => new("John", "Doe", "doe@mail.com", "$2a$12$Eju2KmPviy2UCJIUAlTtr.LFZ/DbdsFOOlN3YEoP5p30HLxwe1YXG")
        {
            Id = new UserId(1),
            Jwt = JwtSeed.Jwt
        };
    }
}
