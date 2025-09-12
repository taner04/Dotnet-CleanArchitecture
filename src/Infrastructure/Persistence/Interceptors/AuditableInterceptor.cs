using Application.Abstraction;
using Application.Abstraction.Infrastructure;
using Domain.Abstraction.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

public class AuditableInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            SetAuditableProperties(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetAuditableProperties(DbContext context)
    {
        var auditableEntities = context.ChangeTracker
            .Entries()
            .Where(e => e.Entity is IAuditable);

        var userId = currentUserService.GetUserId()?.Value.ToString();
        
        foreach (var entry in auditableEntities)
        {
            if (entry.Entity is IAuditable auditable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditable.SetCreated(userId!);
                        break;

                    case EntityState.Modified:
                        auditable.SetUpdated(userId!);
                        break;

                    case EntityState.Deleted:
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    default:
                        break;
                }
            }
        }
    }
}