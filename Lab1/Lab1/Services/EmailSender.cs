using Microsoft.AspNet.Identity;
using System;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security;


namespace Lab1.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailSender()
        {
            Options = new AuthMessageSenderOptions() { SendGridUser = "RedArteleristAPIKey", SendGridKey = "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" };
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Joe@contoso.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }

    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        return Task.FromResult(0);
    //    }

    //    void sendMail(IdentityMessage message)
    //    {
    //        #region formatter
    //        string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
    //        string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

    //        html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
    //        #endregion

    //        MailMessage msg = new MailMessage();
    //        msg.From = new MailAddress("joe@contoso.com");
    //        msg.To.Add(new MailAddress(message.Destination));
    //        msg.Subject = message.Subject;
    //        msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
    //        msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

    //        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
    //        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("joe@contoso.com", "XXXXXX");
    //        smtpClient.Credentials = credentials;
    //        smtpClient.EnableSsl = true;
    //        smtpClient.Send(msg);
    //    }

    //    //private Task configSendGridasync(IdentityMessage message)
    //    //{
    //    //    var myMessage = new SendGridMessage();
    //    //    myMessage.AddTo(message.Destination);
    //    //    myMessage.From = new System.Net.Mail.MailAddress(
    //    //                        "Joe@contoso.com", "Joe S.");
    //    //    myMessage.Subject = message.Subject;
    //    //    myMessage.Text = message.Body;
    //    //    myMessage.Html = message.Body;

    //    //    var credentials = new NetworkCredential(
    //    //               ConfigurationManager.AppSettings["mailAccount"],
    //    //               ConfigurationManager.AppSettings["mailPassword"]
    //    //               );

    //    //    // Create a Web transport for sending email.
    //    //    var transportWeb = new Web(credentials);

    //    //    // Send the email.
    //    //    if (transportWeb != null)
    //    //    {
    //    //        return transportWeb.DeliverAsync(myMessage);
    //    //    }
    //    //    else
    //    //    {
    //    //        return Task.FromResult(0);
    //    //    }
    //    //}
    //}
}
