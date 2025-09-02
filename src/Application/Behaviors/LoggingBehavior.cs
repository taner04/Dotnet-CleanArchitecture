using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public sealed class LoggingBehavior<TMessage, TResponse>(ILogger<LoggingBehavior<TMessage, TResponse>> logger)
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly ILogger<LoggingBehavior<TMessage, TResponse>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing message {@Message}", message);
            return await next(message, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing message {@Message}", message);
            throw;
        }
        finally
        {
            _logger.LogInformation("Finished processing message {@Message}", message);
        }
    }
}