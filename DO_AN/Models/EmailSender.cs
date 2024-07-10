//using MailKit.Net.Smtp;
//using MailKit.Security;
//using Microsoft.Extensions.Options;
//using MimeKit;

//namespace DO_AN.Models
//{
//    public interface IEmailSender
//    {
//        Task SendEmailAsync(string to, string subject, string message);
//    }
//    public class EmailSender : IEmailSender
//    {
//        private readonly SmtpSettings _smtpSettings;

//        public EmailSender(IOptions<SmtpSettings> smtpSettings)
//        {
//            _smtpSettings = smtpSettings.Value;
//        }

//        public async Task SendEmailAsync(string to, string subject, string message)
//        {
//            var emailMessage = new MimeMessage();
//            emailMessage.From.Add(new MailboxAddress("Your App Name", _smtpSettings.Username));
//            emailMessage.To.Add(new MailboxAddress("", to));
//            emailMessage.Subject = subject;
//            emailMessage.Body = new TextPart("html") { Text = message };

//            using var client = new SmtpClient();
//            await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
//            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
//            await client.SendAsync(emailMessage);
//            await client.DisconnectAsync(true);
//        }
//    }
//}