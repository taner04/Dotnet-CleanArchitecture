using Ardalis.Specification;
using Domain.ValueObjects.Identifiers;

namespace Domain.Entities.Carts;

public class CartByUserSpecification : SingleResultSpecification<Cart>
{
    public CartByUserSpecification(UserId userId)
    {
        Query.Where(c => c.UserId == userId)
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product);
    }
}