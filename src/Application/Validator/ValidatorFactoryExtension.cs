using Application.Common.Interfaces;
using FluentValidation.Results;

namespace Application.Validator
{
    /// <summary>
    /// Provides extension methods for <see cref="IValidatorFactory"/>.
    /// </summary>
    public static class ValidatorFactoryExtension
    {
        /// <summary>
        /// Validates the specified object using a validator obtained from the <see cref="IValidatorFactory"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to validate.</typeparam>
        /// <param name="factory">The validator factory instance.</param>
        /// <param name="obj">The object to validate.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> containing the results of the validation.
        /// </returns>
        public static ValidationResult GetResult<T>(this IValidatorFactory factory, T obj) 
            => factory.GetValidator<T>().Validate(obj);
    }
}
