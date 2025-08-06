using Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Base
{
    public abstract class AuditableConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Auditable
    {
        protected const string TimestampWithTimeZone = "timestamp with time zone";

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType(TimestampWithTimeZone);

            builder.Property(e => e.UpdatedAt)
                .IsRequired(required: false)
                .HasColumnType(TimestampWithTimeZone);

            PostConfigure(builder);
        }

        protected abstract void PostConfigure(EntityTypeBuilder<TEntity> builder);
    }
}