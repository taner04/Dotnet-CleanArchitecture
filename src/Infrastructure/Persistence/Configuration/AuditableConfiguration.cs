using Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public abstract class AuditableConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Auditable
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType(Postgres.TimestampWithTimeZone);

            builder.Property(e => e.UpdatedAt)
                .IsRequired(required: false)
                .HasColumnType(Postgres.TimestampWithTimeZone);

            builder.HasQueryFilter(e => !e.IsDeleted);

            PostConfigure(builder);
        }

        protected abstract void PostConfigure(EntityTypeBuilder<TEntity> builder);

        public static class Postgres
        {
            public const string TimestampWithTimeZone = "timestamp with time zone";
            public const string Text = "text";
        }
    }
}