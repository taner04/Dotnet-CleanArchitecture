using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public sealed class UserConfiguration : EntityConfiguration<User, UserId>
{
    protected override string TableName => "Users";

    protected override void PostConfigure(EntityTypeBuilder<User> builder)
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
            .WithOne(a => a.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}