using Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Data.Configuration.Base;
using CartId = Domain.ValueObjects.Identifiers.CartId;

namespace Persistence.Data.Configuration;

public sealed class CartConfiguration : EntityConfiguration<Cart, CartId>
{
    protected override string TabelName => nameof(Cart);

    protected override void PostConfigure(EntityTypeBuilder<Cart> builder)
    {
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