using DO_AN.Models;

namespace DO_AN.Areas.Admin.ViewModels
{
    public class OrderDetailsVM
    {
        public int IdOrder { get; set; }
        public int IdTicket { get; set; }
        public DateTime? Date { get; set; }
        public double? UnitPrice { get; set; }
        public string? NameCus { get; set; }
        public string? Phone { get; set; }
        public int? IdCus { get; set; }
        public IEnumerable<TicketViewModel> Tickets { get; set; } // Danh sách vé

    }
    public class TicketViewModel
    {
        public int IdTicket { get; set; }
        public DateTime? DateStart { get; set; }
        public double Price { get; set; }
        public string SeatName { get; set; } // Tên ghế
        public string PointStart { get; set; } // Điểm xuất phát
        public string PointEnd { get; set; } // Điểm đến
        public decimal? CoefficientTrain { get; set; }
        public string? NameTrain { get; set; }
        public int IdTrain { get; set; } // ID chuyến tàu
    }
}
