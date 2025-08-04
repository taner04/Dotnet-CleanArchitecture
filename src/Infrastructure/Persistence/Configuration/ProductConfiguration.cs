using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class ProductConfiguration : BaseConfiguration<Product, ProductId>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value)
                );

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Stock)
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}

