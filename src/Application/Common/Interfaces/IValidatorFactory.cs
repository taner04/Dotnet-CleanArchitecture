using FluentValidation;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Provides a factory for retrieving validators for specific types.
    /// </summary>
    public interface IValidatorFactory
    {
        /// <summary>
        /// Gets a validator instance for the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to validate.</typeparam>
        /// <returns>An <see cref="IValidator{T}"/> instance for the specified type.</returns>
        IValidator<T> GetValidator<T>();
    }
}
