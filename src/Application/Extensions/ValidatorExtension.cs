using Application.Validator.Costum;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Extensions
{
    public static class ValidatorExtension
    {
        public static IRuleBuilderOptions<T, Guid> IsId<T>(this IRuleBuilder<T, Guid> ruleBuilder)
           => ruleBuilder.SetValidator(new IdValidator<T>());

        public static ValidationResult GetResult<T>(this Common.Interfaces.IValidatorFactory factory, T obj)
           => factory.GetValidator<T>().Validate(obj);
    }
}
