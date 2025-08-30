using Domain.Entities.Products;
using Domain.Identifiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Data.Configuration.Base;

namespace Persistence.Data.Configuration;

public sealed class ProductConfiguration : EntityConfiguration<Product, ProductId>
{
    protected override string TabelName => nameof(Product);

    protected override void PostConfigure(EntityTypeBuilder<Product> builder)
    {
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