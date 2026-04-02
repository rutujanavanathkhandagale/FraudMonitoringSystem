using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress("Fraud Monitoring System", _configuration["EmailSettings:From"]));

        // Recipient
        email.To.Add(new MailboxAddress("", toEmail));
        // Subject
        email.Subject = subject ?? string.Empty;

        // Body (HTML safe)
        email.Body = new TextPart("html")
        {
            Text = htmlMessage ?? string.Empty
        };

        using var smtp = new SmtpClient();

        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

        // CONNECT

        await smtp.ConnectAsync(
             
            "smtp.gmail.com",

            465,

            MailKit.Security.SecureSocketOptions.SslOnConnect

        );

        // AUTH

        await smtp.AuthenticateAsync(

            _configuration["EmailSettings:Username"],

            _configuration["EmailSettings:Password"]

        );

        // SEND

        await smtp.SendAsync(email);

        // DISCONNECT

        await smtp.DisconnectAsync(true);
    }
}
 