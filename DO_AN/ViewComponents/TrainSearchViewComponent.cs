using DO_AN.Models;
using DO_AN.ViewModel.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DO_AN.ViewComponents
{
    public class TrainSearchViewComponent : ViewComponent
    {
        private readonly DOANContext _context;

        public TrainSearchViewComponent(DOANContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string noiDi, string noiDen, DateTime? ngayKhoiHanh, int page = 1,int pageSize=2)
        {
            var veXeQuery = _context.Tickets
                .Include(t => t.IdSeatNavigation)
                .Include(t => t.IdTrainNavigation)
                .ThenInclude(train => train.IdTrainRouteNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(noiDi))
            {
                veXeQuery = veXeQuery.Where(v => v.IdTrainNavigation.IdTrainRouteNavigation.Start_Route.Contains(noiDi));
            }

            if (!string.IsNullOrEmpty(noiDen))
            {
                veXeQuery = veXeQuery.Where(v => v.IdTrainNavigation.IdTrainRouteNavigation.End_Route.Contains(noiDen));
            }
            if (ngayKhoiHanh.HasValue)
            {
                var date = ngayKhoiHanh.Value.Date;
                veXeQuery = veXeQuery.Where(v => v.Date.HasValue && v.Date.Value.Date == date);
            }

            int totalTickets = await veXeQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalTickets / (double)pageSize);

            var pagedTickets = await veXeQuery
                .OrderBy(v => v.IdTicket)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new TicketListViewModel
            {
                Tickets = pagedTickets,
                PagingInfo = new PagingSearch
                {
                    TotalItems = totalTickets,
                    ItemsPerPage = pageSize,
                    CurrentPage = page
                }
            };

            ViewBag.NoiDi = noiDi;
            ViewBag.NoiDen = noiDen;
            ViewBag.NgayKhoiHanh = ngayKhoiHanh;
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View("Default", viewModel); // Return the view component with its corresponding view
        }
    }
}
