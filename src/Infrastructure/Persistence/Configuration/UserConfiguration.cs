using Domain.Entities.Users;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class UserConfiguration : AggregateConfiguration<User>
    {
        protected override string TabelName => nameof(User);

        protected override void PostConfigure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            
            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType(Postgres.TimestampWithTimeZone);
            
            builder.Property(e => e.UpdatedAt)
                .IsRequired(required: false)
                .HasColumnType(Postgres.TimestampWithTimeZone);
            
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
                jwt.ToTable("Jwt");
                
                jwt.Property(j => j.RefreshToken)
                    .IsRequired()
                    .HasColumnName("RefreshToken")
                    .HasColumnType(Postgres.Text);

                jwt.Property(j => j.RefreshTokenExpiration)
                    .IsRequired()
                    .HasColumnName("RefreshTokenExpiration")
                    .HasColumnType(Postgres.TimestampWithTimeZone);
            });
            
            builder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}
