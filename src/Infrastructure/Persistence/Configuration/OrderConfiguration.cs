using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class OrderConfiguration : AuditableConfiguration<Order>
    {
        protected override void PostConfigure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);

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
        }
    }
}
