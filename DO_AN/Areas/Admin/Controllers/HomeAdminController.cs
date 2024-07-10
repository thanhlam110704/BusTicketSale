using DO_AN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;





namespace DO_AN.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        
        private readonly DOANContext _context;
        public HomeAdminController(DOANContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucve")]
        public async Task<IActionResult> DanhMucVe(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstVe = await _context.Tickets
                .Include(t => t.IdSeatNavigation)
                .Include(t => t.IdTrainNavigation)
                .OrderBy(x => x.IdTicket)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(lstVe);
        }


        //[Route("themVeMoi")]
        //[HttpGet]
        //public IActionResult ThemVeMoi()
        //{
        //    ViewBag.IdSeat = new SelectList(_context.Seats.ToList(), "IdSeat", "NameSeat");
        //    ViewBag.IdTrain = new SelectList(_context.Trains.ToList(), "IdTrain", "NameTrain");
        //    return View();
        //}

        //[Route("themVeMoi")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ThemVeMoi([Bind("IdTicket,Date,Price,IdSeat,IdTrain")] Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(ticket);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        ViewBag.MessageFailed = "Failed";
        //    }
        //    ViewData["IdSeat"] = new SelectList(_context.Seats, "IdSeat", "NameSeat", ticket.IdSeat);
        //    ViewData["IdTrain"] = new SelectList(_context.Trains, "IdTrain", "NameTrain", ticket.IdTrain);
        //    return View(ticket);
        //}



        
        [HttpGet("{id}")]
        public async Task<IActionResult> SuaVe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.IdSeatNavigation)
                .Include(t => t.IdTrainNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["IdSeat"] = new SelectList(_context.Seats, "IdSeat", "NameSeat", ticket.IdSeat);
            ViewData["IdTrain"] = new SelectList(_context.Trains, "IdTrain", "NameTrain", ticket.IdTrain);
            return View(ticket);
        }

        // POST: Admin/HomeAdmin/SuaVe/5
        
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaVe(int id, [Bind("IdTicket,Date,Price,IdSeat,IdTrain")] Ticket ticket)
        {
            if (id != ticket.IdTicket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.IdTicket))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DanhMucVe));
            }
            ViewData["IdSeat"] = new SelectList(_context.Seats, "IdSeat", "NameSeat", ticket.IdSeat);
            ViewData["IdTrain"] = new SelectList(_context.Trains, "IdTrain", "NameTrain", ticket.IdTrain);
            return View(ticket);
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.IdTicket == id);
        }


        [Route("xoaVe")]
        [HttpGet]
        public async Task<IActionResult> XoaVe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.IdSeatNavigation)
                .Include(t => t.IdTrainNavigation)
                .FirstOrDefaultAsync(m => m.IdTicket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("XoaVe")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XacNhanXoa(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
