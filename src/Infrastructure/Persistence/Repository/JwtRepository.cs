namespace Infrastructure.Persistence.Repository
{
    public sealed class JwtRepository : Repository<Jwt, JwtId>, IJwtRepository
    {
        public JwtRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
