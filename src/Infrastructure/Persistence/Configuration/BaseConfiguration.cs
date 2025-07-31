using Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    /// <summary>
    /// Abstract base configuration for entity type configuration using Entity Framework Core.
    /// Provides common configuration for entities with an Id, CreatedAt, and UpdatedAt properties.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    public abstract class BaseConfiguration<TEntity, TId> :
        IEntityTypeConfiguration<TEntity> where TEntity : Entity<TId> where TId : struct
    {
        /// <summary>
        /// The SQL column type for timestamp with time zone.
        /// </summary>
        protected const string TimeStampWithTimeZone = "timestamp with time zone";

        /// <summary>
        /// Configures the base entity type for Entity Framework Core.
        /// Sets the table name, primary key, and properties for CreatedAt and UpdatedAt.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);

            builder.HasKey(e => e.Id);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnType(TimeStampWithTimeZone);

            builder.Property<DateTime?>(c => c.UpdatedAt)
                .HasColumnType(TimeStampWithTimeZone);
        }
    }
}
