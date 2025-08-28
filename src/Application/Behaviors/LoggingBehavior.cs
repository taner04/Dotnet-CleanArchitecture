using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public sealed class LoggingBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly ILogger<LoggingBehavior<TMessage, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TMessage, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling message {@Message}", message);

        try
        {
            var response = await next(message, cancellationToken);

            _logger.LogInformation("Handled message {@Message} with response {@Response}", message, response);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error handling message {@Message}", message);
            throw;
        }
        finally
        {
            _logger.LogInformation("Finished processing message {@Message}", message);
        }
    }
}