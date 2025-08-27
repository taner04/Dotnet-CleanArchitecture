using Domain.Entities.Carts;
using Infrastructure.Persistence.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

/// <summary>
/// Entity Framework configuration for the <see cref="Cart"/> aggregate.
/// Configures the table name, primary key, and owned collection of <see cref="CartItem"/>s.
/// </summary>
public sealed class CartConfiguration : AggregateConfiguration<Cart>
{
    /// <summary>
    /// Gets the table name for the <see cref="Cart"/> entity.
    /// </summary>
    protected override string TableName => nameof(Cart);

    /// <summary>
    /// Configures the <see cref="Cart"/> entity and its owned <see cref="CartItem"/> collection.
    /// </summary>
    /// <param name="builder">The entity type builder for <see cref="Cart"/>.</param>
    protected override void PostConfigure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);

        // Configure the owned collection of CartItems
        builder.OwnsMany(c => c.CartItems, cartItems =>
        {
            cartItems.ToTable("CartItems");

            // Unique index on CartId and ProductId
            cartItems.HasIndex("CartId", "ProductId").IsUnique();

            // Foreign key to Cart
            cartItems.WithOwner().HasForeignKey("CartId");
            cartItems.HasKey(i => i.Id);

            // ProductId property configuration
            cartItems.Property(i => i.ProductId)
                .HasColumnName("ProductId");

            // Quantity property configuration
            cartItems.Property(i => i.Quantity)
                .IsRequired();

            // Relationship to Product entity
            cartItems.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}