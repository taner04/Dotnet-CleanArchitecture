using MimeKit;

namespace Application.Common.Interfaces.Infrastructure;

public interface IEmailSender
{
    Task SendAsync(MimeMessage mimeMessage, CancellationToken cancellationToken);
}