using Domain.Entities.Carts;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public sealed class CartConfiguration : AggregateConfiguration<Cart>
{
    protected override string TabelName => nameof(Cart);

    protected override void PostConfigure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);
        
        
        
        builder.OwnsMany(c => c.CartItems, cartItems =>
        {
            cartItems.ToTable("CartItems");
            
            cartItems.HasIndex("CartId", "ProductId").IsUnique();
            
            cartItems.WithOwner().HasForeignKey("CartId");
            cartItems.HasKey(i => i.Id);

            cartItems.Property(i => i.ProductId)
                .HasColumnName("ProductId");
            
            cartItems.Property(i => i.Quantity)
                .IsRequired();

            cartItems.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}