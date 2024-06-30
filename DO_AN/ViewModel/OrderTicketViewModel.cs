using DO_AN.Models;
using System;
using System.Collections.Generic;

namespace DO_AN.ViewModel
{
    public class TrainOrderViewModel
    {
        public Train Train { get; set; }
        public List<Seat> Seats { get; set; }
        public string PointStart { get; set; }
        public string PointEnd { get; set; }
        public string DateStart { get; set; }
        public string NameCoach { get; set; }
        public List<SeatState> SeatState { get; set; }
        public int TicketPrice { get; set; }
    }

    public class SeatState
    {
        public int SeatId { get; set; }
        public string SeatName { get; set; }
        public bool? IsBooked { get; set; }
    }
}
