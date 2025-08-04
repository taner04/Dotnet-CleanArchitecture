using Application.Common.Interfaces.Infrastructure;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Utilities.Email
{
    [ServiceInjection(typeof(IEmailSender), ScopeType.AddTransient)]
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(MimeMessage mimeMessage)
        {
            try
            {
                var smtpConfig = new SmtpConfig(_configuration);
                using var smtpClient = new SmtpClient();

                await smtpClient.ConnectAsync(smtpConfig.Host, smtpConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
    }
}
