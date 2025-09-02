namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>]
public readonly partial struct CartItemId
{
    public static CartItemId New()
    {
        return From(Guid.CreateVersion7());
    }

    public static Validation Validate(Guid cartItemId)
    {
        return cartItemId == Guid.Empty ? Validation.Invalid("The CartItemId cannot be an empty GUID.") : Validation.Ok;
    }
}