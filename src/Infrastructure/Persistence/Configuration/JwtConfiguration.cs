using Infrastructure.Persistence.Configuration.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class JwtConfiguration : BaseConfiguration<Jwt, JwtId>
    {
        public override void Configure(EntityTypeBuilder<Jwt> builder)
        {
            base.Configure(builder);

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
                .HasColumnType(TimeStampWithTimeZone);

            builder.Property(j => j.RefreshTokenExpiration)
                .IsRequired()
                .HasColumnType(TimeStampWithTimeZone);

            builder.Property(j => j.UserId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value)
                );

            builder.HasIndex(j => j.UserId)
                .IsUnique();

            Seed(builder);
        }

        public override void Seed(EntityTypeBuilder<Jwt> builder) => builder.HasData(JwtSeed.Jwt);
    }
}
