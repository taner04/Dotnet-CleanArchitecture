using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Data.Configuration.Base;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Persistence.Data.Configuration;

public sealed class UserConfiguration : EntityConfiguration<User, UserId>
{
    protected override string TableName => nameof(User);

    protected override void PostConfigure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .IsRequired();

        builder.Property(u => u.LastName)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.RefreshToken)
            .IsRequired()
            .HasColumnType(PostgresTypes.Text);

        builder.Property(u => u.RefreshTokenExpiration)
            .IsRequired()
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(u => u.IsDeleted == false);
    }
}