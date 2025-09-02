namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>]
public readonly partial struct UserId
{
    public static UserId New() => From(Guid.CreateVersion7());
    
    public static Validation Validate(Guid userId)
    {
        return userId == Guid.Empty ? Validation.Invalid("The UserId cannot be an empty GUID.") : Validation.Ok;
    }
}