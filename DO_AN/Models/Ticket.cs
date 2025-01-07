using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Ticket
    {
        public int IdTicket { get; set; }
        public DateTime? Date { get; set; }
        public double Price { get; set; }
        public int? IdSeat { get; set; }
        public int? IdTrain { get; set; }
        public int? IdOrder { get; set; }

        public virtual Order? IdOrderNavigation { get; set; }
        public virtual Seat? IdSeatNavigation { get; set; }
        public virtual Train? IdTrainNavigation { get; set; }
    }
}
