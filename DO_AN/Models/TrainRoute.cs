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
        public string? Start_Route { get; set; }
        public string? End_Route { get; set; }

        public virtual ICollection<Train> Trains { get; set; } // Collection of trains on this route

        
    }
}
