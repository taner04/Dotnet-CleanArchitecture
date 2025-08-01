using Infrastructure.Persistence.Configuration.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class OrderConfiguration : BaseConfiguration<Order, OrderId>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(j => j.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value,
                    value => new OrderId(value)
                );

            builder.Property(j => j.UserId)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value)
                );

            builder.Property(o => o.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.OrderDate)
                .IsRequired()
                .HasColumnType(TimeStampWithTimeZone);

            builder.Property(o => o.PaymentMethod)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(o => o.OrderStatus)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();

            Seed(builder);
        }

        public override void Seed(EntityTypeBuilder<Order> builder) => builder.HasData(OrderSeed.Order);
    }
}
