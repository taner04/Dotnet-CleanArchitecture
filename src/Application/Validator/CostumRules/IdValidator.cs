using FluentValidation;
using FluentValidation.Validators;

namespace Application.Validator.CostumRules;

public class IdValidator<T> : PropertyValidator<T, Guid>
{
    public override string Name => "IdValidator";

    public override bool IsValid(ValidationContext<T> context, Guid value)
    {
        return !value.Equals(Guid.Empty);
    }
}