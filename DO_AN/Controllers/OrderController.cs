using DO_AN.Models;
using DO_AN.Services;
using DO_AN.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DO_AN.Controllers
{
    public class OrderController : Controller
    {
        private readonly DOANContext _context;
        private readonly IVNPayService _vnPayService;

        public OrderController(DOANContext context, IVNPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? idTrain)
        {
            if (idTrain == null)
            {
                return NotFound();
            }
            var train = await _context.Trains
                                      .Include(t => t.IdTrainRouteNavigation)
                                      .FirstOrDefaultAsync(t => t.IdTrain == idTrain);
            if (train == null)
            {
                return NotFound();
            }
            // Lấy danh sách ghế đã đặt từ cơ sở dữ liệu
            var coaches = await _context.Coaches
                                        .Where(c => c.IdTrain == idTrain)
                                        .Include(c => c.Seats)
                                        .ToListAsync();
            var occupiedSeats = coaches.SelectMany(c => c.Seats)
                                        .Where(s => s.State == true)
                                        .ToList();

            var viewModel = new OrderTicketViewModel
            {
                Train = train,
                IdTrain = train.IdTrain,
                OccupiedSeats = occupiedSeats,
                PointStart = train.IdTrainRouteNavigation.PointStart,
                PointEnd = train.IdTrainRouteNavigation.PointEnd,
                DateStart = train.DateStart?.ToShortDateString(),
                Price = 100000, // Giá vé mẫu
                VehicleType = "Mecsedec 45 chỗ" // Loại xe mẫu
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string Fullname, string Phone, string Email)
        {
            if (!ModelState.IsValid)
            {
                return View(); // Trả về view nếu dữ liệu không hợp lệ
            }

            var vnPayModel = new VnPayRequestModel
            {

                Amount = 1000000,
                CreatedDate = DateTime.Now, // DateTime.now
                Description = "Thanh toán",
                Fullname = Fullname,
                OrderId = 9457
                // Bạn có thể thêm các thuộc tính khác vào vnPayModel cho việc xử lý thanh toán
            };

            // Lưu thông tin đơn hàng vào cơ sở dữ liệu nếu cần

            // Chuyển hướng đến URL của cổng thanh toán
            return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
        }



        public IActionResult Pay_Fail()
        {
            return View();
        }


        public IActionResult Pay_Success()
        {
            return View();
        }


        public IActionResult PaymentCallBack()
        {
            
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay {response.VnPayResponseCode}";
                return RedirectToAction("Pay_Fail");
            }

            //Luu đơn hàng dô database
            TempData["Message"] = $"Thanh toán thành công";
            return RedirectToAction("Pay_Success");
        }

    }
}
