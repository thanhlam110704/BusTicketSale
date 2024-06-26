using DO_AN.Models;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.ViewModel.PageSearch
{
    public class TicketListViewModel
    {
          
        public IEnumerable<Ticket> Tickets { get; set; } = Enumerable.Empty<Ticket>();

        public PagingSearch PagingInfo { get; set; } = new();

    }
}
