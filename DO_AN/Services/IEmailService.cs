using DO_AN.ViewModel;

namespace DO_AN.Services
{

    public interface IEmailService
    {
        Task SendTicketEmailAsync(string toEmail, string subject, string ticketHtml);
    }


}
