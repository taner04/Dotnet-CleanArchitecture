namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>]
public readonly partial struct ProductId
{
    public static ProductId New()
    {
        return From(Guid.CreateVersion7());
    }

    public static Validation Validate(Guid orderId)
    {
        return orderId == Guid.Empty ? Validation.Invalid("The ProductId cannot be an empty GUID.") : Validation.Ok;
    }
}