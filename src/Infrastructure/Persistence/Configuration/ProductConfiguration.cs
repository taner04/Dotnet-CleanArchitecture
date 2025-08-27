using Domain.Entities.Products;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public sealed class ProductConfiguration : AggregateConfiguration<Product>
{
    protected override string TabelName => nameof(Product);

    protected override void PostConfigure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnType(Postgres.TimestampWithTimeZone);

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false)
            .HasColumnType(Postgres.TimestampWithTimeZone);

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