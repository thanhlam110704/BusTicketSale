using DO_AN.Models;
using DO_AN.ViewModel.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    public class TrainController : Controller
    {
        private readonly DOANContext _context;

        
        public TrainController(DOANContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SearchTrain(string noiDi, string noiDen, DateTime? ngayKhoiHanh, int page = 1)
        {
            ViewBag.NoiDi = noiDi;
            ViewBag.NoiDen = noiDen;
            ViewBag.NgayKhoiHanh = ngayKhoiHanh;
            ViewBag.Page = page;
            return View();
        }
    }
}
