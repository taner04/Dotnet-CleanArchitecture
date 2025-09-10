using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Finance.Configuration;

public sealed class UserConfiguration : EntityConfiguration<User, UserId>
{
    protected override string TableName => "Users";
    
    protected override void PostConfigure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Account)
            .WithOne(a => a.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}