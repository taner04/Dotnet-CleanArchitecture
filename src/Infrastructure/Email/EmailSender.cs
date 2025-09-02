using System.Diagnostics;
using Application.Abstraction.Utils;
using MimeKit;

namespace Infrastructure.Email;

[ServiceInjection(typeof(IEmailSender), ScopeType.Transient)]
public class EmailSender : IEmailSender
{
    //TODO: Maybe implement actual email sending logic using SMTP or any other service.
    public Task SendAsync(MimeMessage mimeMessage, CancellationToken cancellationToken)
    {
        if (mimeMessage is null)
        {
            throw new ArgumentNullException(nameof(mimeMessage), "MimeMessage cannot be null.");
        }

        Debug.WriteLine($"\n\tEmail send to: {mimeMessage.To.First()}", "Domain-Event");
        return Task.CompletedTask;
        //try
        //{
        //    var smtpConfig = new SmtpConfig(_configuration);
        //    using var smtpClient = new SmtpClient();

        //    await smtpClient.ConnectAsync(smtpConfig.Host, smtpConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
        //    await smtpClient.SendAsync(mimeMessage);
        //    await smtpClient.DisconnectAsync(true);
        //}
        //catch (Exception ex)
        //{
        //    throw new InvalidOperationException("Failed to send email", ex);
        //}
    }
}