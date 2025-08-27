using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptor;

public sealed class SoftDeletableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            SetSoftDeleteProperties(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private static void SetSoftDeleteProperties(DbContext context)
    {
        var softDeletableEntities = context.ChangeTracker
                                           .Entries()
                                           .Where(e => e.Entity is ISoftDeletable);
        
        foreach (var entry in softDeletableEntities)
        {
            if(entry.Entity is ISoftDeletable softDeletable)
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        softDeletable.IsDeleted = true;
                        entry.State = EntityState.Modified;
                        break;
                    
                    case EntityState.Added:
                    case EntityState.Modified:
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    default:
                        break;
                }
            }
        }
    }
}