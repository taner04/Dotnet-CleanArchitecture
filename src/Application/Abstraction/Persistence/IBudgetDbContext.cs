using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstraction.Persistence;

public interface IBudgetDbContext : IDisposable
{
    DbSet<User> Users { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    DatabaseFacade Database { get; }
}