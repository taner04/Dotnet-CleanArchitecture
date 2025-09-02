using Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration.Base;

public abstract class EntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IAuditable, IEntity<TId>
    where TId : struct
{
    protected abstract string TableName { get; }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);
        
        builder.Property(e => e.UpdatedAt)
            .HasColumnType(PostgresTypes.TimestampWithTimeZone);

        PostConfigure(builder);
    }

    protected abstract void PostConfigure(EntityTypeBuilder<TEntity> builder);
}