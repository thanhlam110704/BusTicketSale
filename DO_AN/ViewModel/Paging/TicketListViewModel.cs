using DO_AN.Models;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.ViewModel.Paging
{
    public class TicketListViewModel
    {

        public IEnumerable<Ticket> Tickets { get; set; } = Enumerable.Empty<Ticket>();

        public IEnumerable<TrainRoute> TrainRoutes { get; set; } = Enumerable.Empty<TrainRoute>();


        public PagingSearch PagingInfo { get; set; } = new();

    }
}
