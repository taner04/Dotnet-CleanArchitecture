using Application.Common.Interfaces.Infrastructure;
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

        //TODO: Maybe implement actual email sending logic using SMTP or any other service.
        public Task SendAsync(MimeMessage mimeMessage)
        {
            if (mimeMessage is null)
            {
                throw new ArgumentNullException(nameof(mimeMessage), "MimeMessage cannot be null.");
            }

            Console.WriteLine($"Email send to: {mimeMessage.To.First()}");
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
}
