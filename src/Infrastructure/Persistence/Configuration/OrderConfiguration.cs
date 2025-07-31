using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class OrderConfiguration : BaseConfiguration<Order, OrderId>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value,
                    value => new OrderId(value)
                );

            builder.Property(o => o.Amount)
                .IsRequired();

            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasColumnType(TimeStampWithTimeZone);
        
            builder.Property(o => o.PaymentMethod)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.OrderStatus)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
