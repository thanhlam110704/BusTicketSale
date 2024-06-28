using DO_AN.Models;
using DO_AN.ViewModel.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    public class TicketController : Controller
    {
        private readonly DOANContext _context;

        public TicketController(DOANContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SearchTicket(string noiDi, string noiDen, DateTime? ngayKhoiHanh, int page = 1)
        {
            ViewBag.NoiDi = noiDi;
            ViewBag.NoiDen = noiDen;
            ViewBag.NgayKhoiHanh = ngayKhoiHanh;
            ViewBag.Page = page;

            // Thực hiện logic lấy dữ liệu từ _context dựa trên các tham số như noiDi, noiDen, ngayKhoiHanh, page
            // Ví dụ:
            // var tickets = await _context.Tickets.Where(...).ToListAsync();

            // Trả về view với dữ liệu đã lấy được
            return View();
        }
    }
}
