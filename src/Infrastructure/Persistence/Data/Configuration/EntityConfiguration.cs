using Domain.Abstraction.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configuration;

public abstract class EntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> 
    where TEntity : class, IEntity<TId>
    where TId : struct
{
    protected abstract string TableName { get; }
    
    
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);
        
        builder.HasKey(e => e.Id);
        
        PostConfigure(builder);
    }
    
    protected abstract void PostConfigure(EntityTypeBuilder<TEntity> builder);
}