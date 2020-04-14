//using Castle.Core.Smtp;
//using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace Lab1.ViewModels
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Site administration", "fedorenko.max020401@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 25, false);
                await client.AuthenticateAsync("fedorenko.max020401@mail.ru", "ms611001ms");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
    //public class AuthMessageSenderOptions
    //{
    //    public string SendGridUser { get; set; }
    //    public string SendGridKey { get; set; }
    //}
    //public class EmailSender : IEmailSender
    //{
    //    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
    //    {
    //        Options = optionsAccessor.Value;
    //    }

    //    public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

    //    public Task SendEmailAsync(string email, string subject, string message)
    //    {
    //        return Execute(Options.SendGridKey, subject, message, email);
    //    }

    //    public Task Execute(string apiKey, string subject, string message, string email)
    //    {
    //        var client = new SendGridClient(apiKey);
    //        var msg = new SendGridMessage()
    //        {
    //            From = new EmailAddress("Joe@contoso.com", Options.SendGridUser),
    //            Subject = subject,
    //            PlainTextContent = message,
    //            HtmlContent = message
    //        };
    //        msg.AddTo(new EmailAddress(email));

    //        // Disable click tracking.
    //        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
    //        msg.SetClickTracking(false, false);

    //        return client.SendEmailAsync(msg);
    //    }
    //}

    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        return configSendGridasync(message);
    //    }

    //    private Task configSendGridasync(IdentityMessage message)
    //    {
    //        var myMessage = new SendGridMessage();
    //        myMessage.AddTo(message.Destination);
    //        myMessage.From = new System.Net.Mail.MailAddress(
    //                            "Joe@contoso.com", "Joe S.");
    //        myMessage.Subject = message.Subject;
    //        myMessage.Text = message.Body;
    //        myMessage.Html = message.Body;

    //        var credentials = new NetworkCredential(
    //                   ConfigurationManager.AppSettings["mailAccount"],
    //                   ConfigurationManager.AppSettings["mailPassword"]
    //                   );

    //        // Create a Web transport for sending email.
    //        var transportWeb = new Web(credentials);

    //        // Send the email.
    //        if (transportWeb != null)
    //        {
    //            return transportWeb.DeliverAsync(myMessage);
    //        }
    //        else
    //        {
    //            return Task.FromResult(0);
    //        }
    //    }
    //}
}
