namespace DO_AN.Areas.Admin.ViewModels
{
    public class ExportOrdersToCSVViewModel
    {
        public int IdOrder { get; set; }
        public int IdTicket { get; set; }
        public DateTime? BookingDate { get; set; }
        public double? UnitPrice { get; set; }
        public string? NameCus { get; set; }
        public string? Phone { get; set; }
        public int? IdCus { get; set; }
        public DateTime? DateStart { get; set; }
        public double Price { get; set; }
        public string SeatName { get; set; }
        public string PointStart { get; set; }
        public string PointEnd { get; set; }
        public decimal? CoefficientTrain { get; set; }
        public string? NameTrain { get; set; }
        public int IdTrain { get; set; }
    
}
}
