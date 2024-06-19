using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class TrainRoute
    {
        public TrainRoute()
        {
            Trains = new HashSet<Train>();
        }

        public int IdTrainRoute { get; set; }
        public string? NameTrainRoute { get; set; }

        public virtual ICollection<Train> Trains { get; set; }
    }
}
