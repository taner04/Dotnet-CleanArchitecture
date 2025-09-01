using MimeKit;

namespace Application.Abstraction.Utils;

/// <summary>
/// Defines a contract for sending email messages asynchronously.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends the specified <see cref="MimeMessage"/> asynchronously.
    /// </summary>
    /// <param name="mimeMessage">The email message to send.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    Task SendAsync(MimeMessage mimeMessage, CancellationToken cancellationToken);
}