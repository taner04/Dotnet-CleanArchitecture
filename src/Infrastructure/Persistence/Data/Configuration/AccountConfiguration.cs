using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configuration;

public sealed class AccountConfiguration : EntityConfiguration<Account, AccountId>
{
    protected override string TableName => "Accounts";
    
    protected override void PostConfigure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(a => a.Balance)
            .IsRequired();
        
        builder.Property(a => a.UserId)
            .IsRequired();

        builder.HasOne(a => a.User)
            .WithOne(u => u.Account);
        
        builder.HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .OnDelete(DeleteBehavior.Cascade);
    }
}