using Domain.Entities.Users;
using Domain.Identifiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Data.Configuration.Base;

namespace Persistence.Data.Configuration;

public sealed class UserConfiguration : EntityConfiguration<User, UserId>
{
    protected override string TabelName => nameof(User);

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

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.RefreshToken)
            .IsRequired()
            .HasColumnType(PostgresTypes.Text);

        builder.Property(u => u.RefreshTokenExpiration)
            .IsRequired()
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        builder.HasQueryFilter(u => u.IsDeleted == false);
    }
}