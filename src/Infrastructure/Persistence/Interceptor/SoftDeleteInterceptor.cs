using Domain.Common.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptor
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
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

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                SetSoftDeleteProperties(eventData.Context);
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private static void SetSoftDeleteProperties(DbContext context)
        {
            var softDeletableEntities = context.ChangeTracker.Entries()
                .Where(e => e.Entity is ISoftDeletable && e.State == EntityState.Deleted);

            foreach (var entry in softDeletableEntities)
            {
                if (entry.Entity is ISoftDeletable softDeletable)
                {
                    softDeletable.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
