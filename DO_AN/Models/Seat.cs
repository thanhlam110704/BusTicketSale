using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Seat
    {
        public Seat()
        {
            Coaches = new HashSet<Coach>();
            Tickets = new HashSet<Ticket>();
        }

        public int IdSeat { get; set; }
        public string? NameSeat { get; set; }
        public bool? State { get; set; }

        public virtual ICollection<Coach> Coaches { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
