using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class UserConfiguration : AuditableConfiguration<User>
    {
        protected override void PostConfigure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.FirstName)
                .IsRequired();

            builder.Property(u => u.LastName)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();
            
            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.OwnsOne(u => u.Jwt, jwt =>
            {
                jwt.Property(j => j.Token)
                    .IsRequired()
                    .HasColumnName("JwtToken")
                    .HasColumnType(Postgres.Text);

                jwt.Property(j => j.TokenExpiration)
                    .IsRequired()
                    .HasColumnName("JwtTokenExpiration")
                    .HasColumnType(Postgres.TimestampWithTimeZone);

                jwt.Property(j => j.RefreshToken)
                    .IsRequired()
                    .HasColumnName("JwtRefreshToken")
                    .HasColumnType(Postgres.Text);

                jwt.Property(j => j.RefreshTokenExpiration)
                    .IsRequired()
                    .HasColumnName("JwtRefreshTokenExpiration")
                    .HasColumnType(Postgres.TimestampWithTimeZone);
            });
        }
    }
}
