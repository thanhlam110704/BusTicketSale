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

        public async Task<IViewComponentResult> InvokeAsync(string noiDi, string noiDen, DateTime? ngayKhoiHanh, int page = 1, int pageSize = 2)
        {
            var chuyenxeQuery = _context.Trains
                .Include(t => t.IdTrainRouteNavigation).AsQueryable();

            // Apply filters only when provided
            if (!string.IsNullOrEmpty(noiDi))
            {
                chuyenxeQuery = chuyenxeQuery.Where(v => v.IdTrainRouteNavigation.PointStart.Contains(noiDi));
            }
            if (!string.IsNullOrEmpty(noiDen))
            {
                chuyenxeQuery = chuyenxeQuery.Where(v => v.IdTrainRouteNavigation.PointEnd.Contains(noiDen));
            }
            if (ngayKhoiHanh.HasValue)
            {
                var date = ngayKhoiHanh.Value.Date;
                chuyenxeQuery = chuyenxeQuery.Where(v => v.DateStart.HasValue && v.DateStart.Value.Date == date);
            }

            int totalItem = await chuyenxeQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItem / (double)pageSize);

            var pagedTickets = await chuyenxeQuery
                .OrderBy(v => v.IdTrain)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new TrainListViewModel
            {
                Trains = pagedTickets,
                PagingInfo = new PagingSearch
                {
                    TotalItems = totalItem,
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
