using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Train
    {
        public Train()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdTrain { get; set; }
        public string? NameTrain { get; set; }
        public DateTime? DateStart { get; set; }
        public int IdCoach { get; set; }
        public int IdTrainRoute { get; set; }

        public virtual Coach IdCoachNavigation { get; set; } // Assuming Coach model is defined elsewhere
        public virtual TrainRoute IdTrainRouteNavigation { get; set; } // Navigation property to TrainRoute
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
