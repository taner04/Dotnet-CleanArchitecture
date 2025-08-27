using Domain.Entities.Products;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

/// <summary>
/// Entity Framework configuration for the <see cref="Product"/> aggregate.
/// Defines table name, property mappings, constraints, and query filters.
/// </summary>
public sealed class ProductConfiguration : AggregateConfiguration<Product>
{
    /// <summary>
    /// Gets the table name for the <see cref="Product"/> entity.
    /// </summary>
    protected override string TableName => nameof(Product);

    /// <summary>
    /// Configures entity properties, keys, and query filters for <see cref="Product"/>.
    /// </summary>
    /// <param name="builder">The entity type builder for <see cref="Product"/>.</param>
    protected override void PostConfigure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false)
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Quantity)
            .IsRequired();

        builder.HasQueryFilter(p => p.IsDeleted == false);
    }
}