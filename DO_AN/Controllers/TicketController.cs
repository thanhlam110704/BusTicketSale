using DO_AN.Models;
using DO_AN.ViewModel.PageSearch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DO_AN.Controllers
{
    public class TicketController : Controller
    {
        private readonly DOANContext _context;

        public TicketController(DOANContext context)
        {
            _context = context;
        }

        public IActionResult SearchTicket(string noiDi, string noiDen, DateTime? ngayKhoiHanh, int page = 1)
        {
            int pageSize = 2; // Số lượng vé trên mỗi trang

            var veXeQuery = _context.Tickets
             .Include(t => t.IdSeatNavigation)
             .Include(t => t.IdTrainNavigation)
             .ThenInclude(train => train.IdTrainRouteNavigation)
             .AsQueryable();


            // Áp dụng các bộ lọc chỉ khi chúng được cung cấp
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

            int totalTickets = veXeQuery.Count();
            int totalPages = (int)Math.Ceiling(totalTickets / (double)pageSize);

            var pagedTickets = veXeQuery
                .OrderBy(v => v.IdTicket)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

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

            return View(viewModel);
        }

    }
}
