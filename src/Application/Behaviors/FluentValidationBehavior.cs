using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public sealed class FluentValidationBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly ILogger<IPipelineBehavior<TMessage, TResponse>> _logger;

    public FluentValidationBehavior(
        IServiceProvider serviceProvider, 
        ILogger<IPipelineBehavior<TMessage, TResponse>> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_serviceProvider.GetService(typeof(IValidator<TMessage>)) is not IValidator<TMessage> validator)
        {
            return await next(message, cancellationToken);
        }

        var context = new ValidationContext<TMessage>(message);
        
        _logger.LogInformation("Validating message {@Message} with {Validator}", message, validator.GetType().Name);
        var validationResult = await validator.ValidateAsync(context, cancellationToken);

        if (validationResult.IsValid) return await next(message, cancellationToken);
        
        _logger.LogError("Validation failed for message {@Message} with errors: {Errors}", message, validationResult.Errors);
        
        return CreateFailureResponse(
            ErrorFactory.ValidationError(validationResult.ToDictionary())
        );
    }
    
    private static TResponse CreateFailureResponse(Error error)
    {
        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(error);
        }

        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(ResultT<>))
        {
            var failure = typeof(ResultT<>)
                .MakeGenericType(typeof(TResponse).GetGenericArguments()[0])
                .GetMethod("Failure")!
                .Invoke(null, [error]);

            return (TResponse)failure!;
        }

        throw new InvalidOperationException($"TResponse must be Result or ResultT<T>, but was {typeof(TResponse).Name}");
    }
}