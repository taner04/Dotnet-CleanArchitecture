namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>]
public readonly partial struct OrderItemId
{
    public static OrderItemId New() => From(Guid.CreateVersion7());
    
    public static Validation Validate(Guid orderItemId)
    {
        return orderItemId == Guid.Empty
            ? Validation.Invalid("The OrderItemId cannot be an empty GUID.")
            : Validation.Ok;
    }
}