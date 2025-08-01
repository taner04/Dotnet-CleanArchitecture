using Domain.Common.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptor
{
    public sealed class UpdateAuditableInterceptor : SaveChangesInterceptor
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

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
            {
                SetAuditableProperties(eventData.Context);
            }

            return base.SavingChanges(eventData, result);
        }

        private static void SetAuditableProperties(DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is IAuditable auditable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditable.CreatedAt = utcNow;
                            auditable.UpdatedAt = utcNow;
                            break;

                        case EntityState.Modified:
                            auditable.UpdatedAt = utcNow;
                            break;
                    }
                }

                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeletable softDeletable)
                {
                    entry.State = EntityState.Modified;
                    softDeletable.IsDeleted = true;

                    if (softDeletable is IAuditable auditableDelete)
                    {
                        auditableDelete.UpdatedAt = utcNow;
                    }
                }
            }
        }

    }
}
