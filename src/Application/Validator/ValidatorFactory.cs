using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Validator;

[ServiceInjection(typeof(IValidatorFactory), ScopeType.Transient)]
public sealed class ValidatorFactory : IValidatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public FluentValidation.IValidator<T> GetValidator<T>()
    {
        var validatorType = typeof(FluentValidation.IValidator<T>);

        var validator = _serviceProvider.GetRequiredService(validatorType) ??
                        throw new InvalidOperationException($"No validator found for type {typeof(T).FullName}");

        if (validator is not FluentValidation.IValidator<T> typedValidator)
            throw new InvalidOperationException($"No validator found for type {typeof(T).FullName}");

        return typedValidator;
    }
}