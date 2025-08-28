using MimeKit;

namespace Application.Abstraction.Utils;

public interface IEmailSender
{
    Task SendAsync(MimeMessage mimeMessage, CancellationToken cancellationToken);
}