using FluentValidation;
using FluentValidation.Results;
using Mediator;

namespace Application.Behaviours;

public class FluentValidationBehaviour<TMessage, TResponse>(IValidator<TResponse>? validator) : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if(validator is null)
        {
            return await next(message, cancellationToken);
        }

        var context = new ValidationContext<TMessage>(message);
        var result = await validator.ValidateAsync(context, cancellationToken);

        if (result.IsValid)
        {
            return await next(message, cancellationToken);
        }

        throw new Exception();
    }
}