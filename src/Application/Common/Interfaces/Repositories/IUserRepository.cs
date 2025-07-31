namespace Application.Common.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
