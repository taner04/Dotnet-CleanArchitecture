using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

            var entries = context.ChangeTracker.Entries()
                    .Where(e => e.Entity is Domain.Common.Interfaces.IAuditable &&
                                (e.State == EntityState.Added || e.State == EntityState.Modified));

            if (entries.Any())
            {
                foreach (EntityEntry<IAuditable> entry in entries.Cast<EntityEntry<IAuditable>>())
                {
                    var entity = entry.Entity;
                    if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = utcNow;
                    }

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = utcNow;
                        entity.UpdatedAt = utcNow;
                    }
                }
            }
        }
    }
}
