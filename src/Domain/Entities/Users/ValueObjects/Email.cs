using System.ComponentModel.DataAnnotations;
using Vogen;

namespace Domain.Entities.Users.ValueObjects;

[ValueObject<string>]
public readonly partial struct Email
{
    public static Validation Validate(string email)
    {
        return new EmailAddressAttribute().IsValid(email) ? Validation.Ok : Validation.Invalid("Invalid email format.");
    }
}