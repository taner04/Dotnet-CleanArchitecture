namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>]
public readonly partial struct CartId
{
    public static CartId New() => From(Guid.CreateVersion7());
    
    public static Validation Validate(Guid cartId)
    {
        return cartId == Guid.Empty ? Validation.Invalid("The CartId cannot be an empty GUID.") : Validation.Ok;
    }
}