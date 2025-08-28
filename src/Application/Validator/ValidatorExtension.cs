using Application.Validator.CostumRules;

namespace Application.Validator;

public static class ValidatorExtension
{
    public static IRuleBuilderOptions<T, Guid> IsId<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new IdValidator<T>());
    }
}