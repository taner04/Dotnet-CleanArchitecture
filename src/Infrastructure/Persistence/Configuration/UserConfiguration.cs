using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class UserConfiguration : AuditableConfiguration<User>
    {
        protected override void PostConfigure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasOne(u => u.Jwt)
                .WithOne(j => j.User)
                .HasForeignKey<Jwt>(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
