using DO_AN.Models;
using DO_AN.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Controllers
{
    public class OrderController : Controller
    {
        private readonly DOANContext _context;

        public OrderController(DOANContext context)
        {
            _context = context;
        }
        public IActionResult Order()
        {
            // Logic để lấy dữ liệu và hiển thị trang order
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index(int idTrain)
        {
            var train = await _context.Trains
                .Include(t => t.IdTrainRouteNavigation)
                .Include(t => t.IdCoachNavigation)
                .ThenInclude(c => c.IdSeatNavigation)
                .FirstOrDefaultAsync(t => t.IdTrain == idTrain);

            //if (train == null)
            //{
            //    return NotFound();
            //}

            //var seats = await _context.Seats
            //    .Where(s => s.IdSeat == train.IdCoachNavigation.IdSeatNavigation.IdSeat)
            //    .ToListAsync();

            var viewModel = new TrainOrderViewModel
            {
                Train = train,
                // Seats = seats,
                PointStart = train.IdTrainRouteNavigation.PointStart,
                PointEnd = train.IdTrainRouteNavigation.PointEnd,
                DateStart = train.DateStart?.ToShortDateString(),
                NameCoach = train.IdCoachNavigation.NameCoach,
                //SeatState = seats.Select(s => new SeatState
                //{
                //    SeatId = s.IdSeat,
                //    SeatName = s.NameSeat,
                //    IsBooked = s.State
                //}).ToList(),
                TicketPrice = 100000 // Assuming a fixed price for demonstration; adjust as needed
            };



            return View( viewModel);
            
        }
    }
}

