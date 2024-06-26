using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Coach
    {
        public Coach()
        {
            Trains = new HashSet<Train>();
        }

        public int IdCoach { get; set; }
        public string? NameCoach { get; set; }
        public string? Category { get; set; }
        public int? SeatsQuantity { get; set; }
        public int? IdSeat { get; set; }

        public virtual Seat? IdSeatNavigation { get; set; }
        public virtual ICollection<Train> Trains { get; set; }
    }
}
