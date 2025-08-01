using FluentValidation;

namespace Application.Service
{
    public abstract class Service
    {
        private readonly IServiceProvider _serviceProvider;
        
        protected Service(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected IValidator<T> GetValidator<T>()
        {
            var validator = _serviceProvider.GetService(typeof(IValidator<T>));
            
            if(validator is null)
            {
                throw new InvalidOperationException($"Validator for type {typeof(T).Name} not found.");
            }

            return (IValidator<T>)validator;
        }
    }
}
