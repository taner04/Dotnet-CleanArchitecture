namespace Application.CQRS.Behaviors;

public sealed class ValidationBehavior<TMessage, TResponse>(IEnumerable<IValidator<TMessage>> validators)
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next(message, cancellationToken);

        var context = new ValidationContext<TMessage>(message);
        var results = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = results
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count == 0) return await next(message, cancellationToken);

        var dict = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(f => f.ErrorMessage).ToArray());

        var error = ErrorFactory.ValidationError(dict);

        return (TResponse)(typeof(TResponse) switch
        {
            var t when t == typeof(Result)
                => Result.Failure(error),

            { IsGenericType: true } t when t.GetGenericTypeDefinition() == typeof(ResultT<>)
                => CreateGenericFailure(t, error),

            _ => throw new InvalidOperationException(
                $"Unsupported response type: {typeof(TResponse).Name}")
        });
    }

    private static object CreateGenericFailure(Type tResponse, Error error)
    {
        var valueType = tResponse.GenericTypeArguments[0];

        var method = typeof(ResultT<>)
            .MakeGenericType(valueType)
            .GetMethod(nameof(ResultT<object>.Failure))!;

        return method.Invoke(null, [error])!;
    }
}