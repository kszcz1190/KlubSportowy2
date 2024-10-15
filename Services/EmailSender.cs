using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace KlubSportowy.Services;
public class EmailSender : IEmailSender
{
    private readonly string smtpHost;
    private readonly int smtpPort;
    private readonly string smtpUsername;
    private readonly string smtpPassword;

    public EmailSender(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
    {
        this.smtpHost = smtpHost;
        this.smtpPort = smtpPort;
        this.smtpUsername = smtpUsername;
        this.smtpPassword = smtpPassword;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("YourAppName", smtpUsername));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(smtpUsername, smtpPassword);

            await client.SendAsync(message);
            client.Disconnect(true);
        }
    }
}
