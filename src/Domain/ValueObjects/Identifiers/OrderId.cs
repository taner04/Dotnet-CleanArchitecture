using Domain.Entities.Users;

namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>]
public readonly partial struct OrderId
{
    public static OrderId New()
    {
        return From(Guid.CreateVersion7());
    }

    public static Validation Validate(Guid orderId)
    {
        return orderId == Guid.Empty ? Validation.Invalid("The OrderId cannot be an empty GUID.") : Validation.Ok;
    }
}