using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value, 
                    value => new UserId(value)
                );

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property<DateTime?>(c => c.UpdatedAt)
                .HasColumnType("timestamp with time zone");
        }
    }
}
