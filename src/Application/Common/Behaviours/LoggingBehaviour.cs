using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public sealed class LoggingBehaviour<TMessage, TResponse>(
    ILogger<IPipelineBehavior<TMessage, TResponse>> logger) : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Processing message {@Message}", message);
            return await next(message, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing message {@Message}", message);
            throw;
        }
        finally
        {
            logger.LogInformation("Finished processing message {@Message}", message);
        }
    }
}