using Domain.Entities.Users;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

/// <summary>
/// Entity Framework configuration for the <see cref="User"/> aggregate.
/// Defines table name, property mappings, owned types, and query filters.
/// </summary>
public sealed class UserConfiguration : AggregateConfiguration<User>
{
    /// <summary>
    /// Gets the table name for the <see cref="User"/> entity.
    /// </summary>
    protected override string TableName => nameof(User);

    /// <summary>
    /// Configures the <see cref="User"/> entity properties, owned types, and query filters.
    /// </summary>
    /// <param name="builder">The entity type builder for <see cref="User"/>.</param>
    protected override void PostConfigure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false)
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

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

        // Configure the owned Jwt value object
        builder.OwnsOne(u => u.Jwt, jwt =>
        {
            jwt.ToTable("Jwt");

            jwt.Property(j => j.RefreshToken)
                .IsRequired()
                .HasColumnName("RefreshToken")
                .HasColumnType(PostgresTypes.Text);

            jwt.Property(j => j.RefreshTokenExpiration)
                .IsRequired()
                .HasColumnName("RefreshTokenExpiration")
                .HasColumnType(PostgresTypes.TimestampWithTimeZone);
        });

        // Apply a query filter to exclude soft-deleted users
        builder.HasQueryFilter(u => u.IsDeleted == false);
    }
}