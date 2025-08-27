using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Base;

public abstract class AggregateConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : class
{
    protected abstract string TabelName { get; }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TabelName);
        
        PostConfigure(builder);
    }
    
    protected abstract void PostConfigure(EntityTypeBuilder<TEntity> builder);
    
    public static class Postgres
    {
        public const string TimestampWithTimeZone = "timestamp with time zone";
        public const string Text = "text";
    }
}