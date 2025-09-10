using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Finance.Configuration;

public sealed class TransactionConfiguration : EntityConfiguration<Transaction, TransactionId>
{
    protected override string TableName => "Transactions";
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