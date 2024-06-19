using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Seat
    {
        public int IdSeat { get; set; }
        public string? NameSeat { get; set; }
        public bool? Sate { get; set; }
        public int IdCoach { get; set; }

        public virtual Coach IdCoachNavigation { get; set; } = null!;
    }
}
