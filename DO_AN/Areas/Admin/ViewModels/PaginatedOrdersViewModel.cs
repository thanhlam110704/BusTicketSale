namespace DO_AN.Areas.Admin.ViewModels
{
    public class PaginatedOrdersViewModel
    {
        public List<OrderDetailsVM> Orders { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
