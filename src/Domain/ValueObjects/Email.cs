using System.ComponentModel.DataAnnotations;

namespace Domain.ValueObjects;

[ValueObject<string>]
public readonly partial struct Email
{
    public static Validation Validate(string value)
    {
        return new EmailAddressAttribute().IsValid(value) ? Validation.Ok : Validation.Invalid("Invalid email format.");
    }
}