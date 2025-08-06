using Domain.Common.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    public abstract class AuditableConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
        where TEntity : class, IAuditable
    {
        protected const string TimestampWithTimeZone = "timestamp with time zone";

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType(TimestampWithTimeZone);

            builder.Property(e => e.UpdatedAt)
                .HasColumnType(TimestampWithTimeZone);
        }

        protected abstract void PostConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
