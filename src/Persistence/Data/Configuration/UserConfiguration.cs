using Domain.Entities.ApplicationUsers;
using UserId = Domain.Entities.ApplicationUsers.UserId;

namespace Persistence.Data.Configuration;

public sealed class UserConfiguration : EntityConfiguration<ApplicationUser, UserId>
{
    protected override void PostConfigure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
            .IsRequired();

        builder.Property(u => u.LastName)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.WantsEmailNotifications)
            .IsRequired();

        builder.HasOne(u => u.Account)
            .WithOne(a => a.ApplicationUser)
            .OnDelete(DeleteBehavior.Cascade);
    }
}