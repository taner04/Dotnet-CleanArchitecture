using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class JwtConfiguration : AuditableConfiguration<Jwt>
    {
        protected override void PostConfigure(EntityTypeBuilder<Jwt> builder)
        {
            builder.HasKey(j => j.Id);

            builder.Property(j => j.Token)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(j => j.RefreshToken)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(j => j.Expiration)
                .IsRequired()
                .HasColumnType(TimestampWithTimeZone);

            builder.Property(j => j.RefreshTokenExpiration)
                .IsRequired()
                .HasColumnType(TimestampWithTimeZone);
        }
    }
}
