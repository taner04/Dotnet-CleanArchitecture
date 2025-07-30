using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class JwtConfiguration : IEntityTypeConfiguration<Jwt>
    {
        public void Configure(EntityTypeBuilder<Jwt> builder)
        {
            builder.ToTable(nameof(Jwt));

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value,
                    value => new JwtId(value)
                );

            builder.Property(j => j.Token)
                .IsRequired()
                .HasMaxLength(1024);

            builder.Property(j => j.RefreshToken)
                .IsRequired()
                .HasMaxLength(1024);

            builder.Property(j => j.Expiration)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(j => j.RefreshTokenExpiration)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(j => j.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property<DateTime?>(j => j.UpdatedAt)
                .HasColumnType("timestamp with time zone");

            builder.Property(j => j.UserId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value)
                );

            builder.HasIndex(j => j.UserId).IsUnique(); // 1:1 Sicherung

        }
    }
}
