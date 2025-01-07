using DO_AN.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


public class EmailService : IEmailService
{
    private readonly string _fromEmail = "nhattan425@gmail.com";
    private readonly string _fromPassword = "chyx hwob foaq bjyr"; // Use a more secure method for storing passwords
    private readonly string _fromDisplayName = "Web Sales Ticket";

    public async Task SendTicketEmailAsync(string toEmail, string subject, string ticketHtml)
    {
        var smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_fromEmail, _fromPassword)
        };

        var fromAddress = new MailAddress(_fromEmail, _fromDisplayName);
        var toAddress = new MailAddress(toEmail);

        var mailMessage = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = ticketHtml,
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}
