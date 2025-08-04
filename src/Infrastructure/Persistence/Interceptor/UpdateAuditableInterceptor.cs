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

        private static void SetAuditableProperties(DbContext context)
        {
            DateTime utcNow = DateTime.UtcNow;

            var auditableEntities = context.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && (e.State == EntityState.Added || e.State == EntityState.Modified));
            
            foreach (var entry in auditableEntities)
            {
                if(entry.Entity is IAuditable auditable)
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
            }
        }

    }
}
