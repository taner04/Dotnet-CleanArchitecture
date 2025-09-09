using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configuration;

public sealed class UserConfiguration : EntityConfiguration<User, UserId>
{
    protected override string TableName => "Users";
    
    protected override void PostConfigure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Firstname)
            .IsRequired();

        builder.Property(u => u.Lastname)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.HasOne(u => u.Account)
            .WithOne(a => a.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}