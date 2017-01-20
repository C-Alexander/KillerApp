using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Shadow_Arena.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var senddomain = "shadowbeta@teamcorgi.com";
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Shadowbeta Team", senddomain));
            mail.Subject = subject;
            mail.Body = new TextPart(TextFormat.Plain.ToString()) { Text = message };
            mail.To.Add(new MailboxAddress(email));

            using (var client = new SmtpClient())
            {
                client.LocalDomain = senddomain;
                client.Connect("smtp.sparkpostmail.com", 587, SecureSocketOptions.StartTls); //example I've found used configureawait todo: figure out the bones of awaits, especially configureawait
                client.Authenticate("SMTP_Injection", "a080d2051742d7efeaca2db3177dc4ed8d94809f");
                client.Send(mail);
                client.DisconnectAsync(true); //todo: check performance implications of not awaiting. It's a task so it should run in the background, but make sure it doesnt affect players

            }
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
