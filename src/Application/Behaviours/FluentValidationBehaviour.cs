using ErrorOr;
using FluentValidation;
using Mediator;

namespace Application.Behaviours;

public class FluentValidationBehaviour<TMessage, TResponse>(IServiceProvider serviceProvider) : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : IErrorOr
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if (serviceProvider.GetService(typeof(IValidator<TMessage>)) is not IValidator<TMessage> validator)
        {
            return await next(message, cancellationToken);
        }

        var context = new ValidationContext<TMessage>(message);
        var validationResult = await validator.ValidateAsync(context, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next(message, cancellationToken);
        }

        var errors = validationResult.Errors
            .ConvertAll(error => Error.Validation(
                code: error.PropertyName,
                description: error.ErrorMessage));

        return (dynamic)errors;
    }
}