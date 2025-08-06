using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class OrderConfiguration : AuditableConfiguration<Order>
    {
        protected override void PostConfigure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Amount)
                .IsRequired();

            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasColumnType(TimestampWithTimeZone);

            builder.Property(o => o.PaymentMethod)
                .IsRequired();

            builder.Property(o => o.OrderStatus)
                .IsRequired();

            builder.Property(o => o.TrackingNumber)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();
        }
    }
}
