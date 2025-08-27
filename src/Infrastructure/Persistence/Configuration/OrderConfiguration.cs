using Domain.Common.Base;
using Domain.Entities.Orders;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public sealed class OrderConfiguration : AggregateConfiguration<Order>
{
    protected override string TabelName => nameof(Order);

    protected override void PostConfigure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnType(Postgres.TimestampWithTimeZone);

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false)
            .HasColumnType(Postgres.TimestampWithTimeZone);

        builder.OwnsMany(o => o.OrderItems, orderItems =>
        {
            orderItems.ToTable("OrderItems");

            orderItems.WithOwner().HasForeignKey("OrderId");
            orderItems.HasKey(i => i.Id);

            orderItems.Property(i => i.ProductId)
                .HasColumnName("ProductId");

            orderItems.Property(i => i.Quantity)
                .IsRequired();

            orderItems.Property(i => i.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            orderItems.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.HasQueryFilter(p => p.IsDeleted == false);
    }
}