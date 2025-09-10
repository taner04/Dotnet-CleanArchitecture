using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstraction;

public interface IDbContext : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    DatabaseFacade Database { get; }
}