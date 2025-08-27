using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Base;

/// <summary>
/// Base configuration class for aggregate entities in Entity Framework.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public abstract class AggregateConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Gets the table name for the entity.
    /// </summary>
    protected abstract string TableName { get; }

    /// <summary>
    /// Configures the entity type by setting the table name and applying additional configuration.
    /// </summary>
    /// <param name="builder">The builder to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);

        PostConfigure(builder);
    }

    /// <summary>
    /// Performs additional configuration for the entity type.
    /// </summary>
    /// <param name="builder">The builder to configure the entity type.</param>
    protected abstract void PostConfigure(EntityTypeBuilder<TEntity> builder);
}