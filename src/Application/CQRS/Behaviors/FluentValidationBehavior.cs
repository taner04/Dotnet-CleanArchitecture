using Org.BouncyCastle.Tls;
using ValidationException = Domain.Exceptions.ValidationException;

namespace Application.CQRS.Behaviors;

public sealed class FluentValidationBehavior<TMessage, TResponse>(IServiceProvider serviceProvider)
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly IServiceProvider _serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_serviceProvider.GetService(typeof(IValidator<TMessage>)) is not IValidator<TMessage> validator)
            return await next(message, cancellationToken);

        var context = new ValidationContext<TMessage>(message);
        var validationResult = await validator.ValidateAsync(context, cancellationToken);

        if (validationResult.IsValid) return await next(message, cancellationToken);

        throw new ValidationException(
            ErrorFactory.ValidationError(validationResult.ToDictionary())
        );
    }
}