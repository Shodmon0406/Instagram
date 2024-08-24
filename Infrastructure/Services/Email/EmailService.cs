using Domain.Dtos.EmailDto;
using Domain.Dtos.MessagesDto;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services.Email;

public class EmailService(EmailConfiguration configuration, ILogger<EmailService> logger)
    : IEmailService
{
    public void SendEmail(MessagesDto message,TextFormat format)
    {
        var emailMessage = CreateEmailMessage(message,format);
        Send(emailMessage);
    }
    
    private MimeMessage CreateEmailMessage(MessagesDto message,TextFormat format)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("mail",configuration.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(format) { Text = message.Content };

        return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(configuration.SmtpServer, configuration.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(configuration.UserName, configuration.Password);

            client.Send(mailMessage);
        }
        catch (Exception e)
        {
            logger.LogError("Error in the send message service: {Message}", e.Message);
        }
        finally
        {
            client.Disconnect(true);
        }
    }
    
}