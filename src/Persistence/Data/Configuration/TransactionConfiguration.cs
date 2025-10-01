using Domain.Entities.ApplicationUsers;
using TransactionId = Domain.Entities.ApplicationUsers.TransactionId;

namespace Persistence.Data.Configuration;

public sealed class TransactionConfiguration : EntityConfiguration<Transaction, TransactionId>
{
    protected override void PostConfigure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(t => t.AccountId)
            .IsRequired();

        builder.Property(t => t.Amount)
            .IsRequired();

        builder.Property(t => t.Type)
            .IsRequired();

        builder.HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .OnDelete(DeleteBehavior.Cascade);
    }
}