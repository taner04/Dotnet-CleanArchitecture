using Domain.Common.Base;
using Domain.Entities.Orders;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

/// <summary>
/// Entity Framework configuration for the <see cref="Order"/> aggregate.
/// Defines table mapping, property configurations, owned entities, and query filters.
/// </summary>
public sealed class OrderConfiguration : AggregateConfiguration<Order>
{
    /// <summary>
    /// Gets the table name for the <see cref="Order"/> entity.
    /// </summary>
    protected override string TableName => nameof(Order);

    /// <summary>
    /// Configures the <see cref="Order"/> entity and its owned <see cref="OrderItem"/> entities.
    /// </summary>
    /// <param name="builder">The entity type builder for <see cref="Order"/>.</param>
    protected override void PostConfigure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false)
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        // Configure owned collection of OrderItems
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