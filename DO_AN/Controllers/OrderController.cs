using DO_AN.Models;
using DO_AN.Services;
using DO_AN.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DOAN_BoSungContext _context;
        private readonly IVNPayService _vnPayService;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;


        public OrderController(DOAN_BoSungContext context, IVNPayService vnPayService, IHttpContextAccessor httpContext, IEmailService emailService, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContext;
            _context = context;
            _vnPayService = vnPayService;
            _emailService = emailService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? idCoach)
        {
            //Kiểm tra đăng nhập
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Access");
            }
            //Lấy id được lưu 
            HttpContext.Session.SetInt32("idCoach", (int)idCoach);

            if (idCoach == null)
            {
                return NotFound();
            }

            var coach = await GetCoachWithRelatedDataAsync(idCoach.Value);
            if (coach == null)
            {
                return NotFound();
            }

            var occupiedSeats = GetOccupiedSeats(coach.Seats);
            var coachCategory = coach.Category;
            var ticketPrice = CalculateTicketPrice(coach.IdTrainNavigation);

            var viewModel = CreateOrderTicketViewModel(coach, occupiedSeats, coachCategory, ticketPrice);

            return View(viewModel);
        }

        private async Task<Coach> GetCoachWithRelatedDataAsync(int idCoach)
        {
            return await _context.Coaches
                                 .Include(c => c.Seats)
                                 .Include(c => c.IdTrainNavigation)
                                    .ThenInclude(t => t.IdTrainRouteNavigation)
                                 .FirstOrDefaultAsync(c => c.IdCoach == idCoach);
        }

        private List<Seat> GetOccupiedSeats(IEnumerable<Seat> seats)
        {
            return seats.Where(s => s.State).ToList();
        }

        private decimal CalculateTicketPrice(Train train)
        {
            var basicPrice = (decimal)(train.Coaches.FirstOrDefault()?.BasicPrice ?? 0);
            return (decimal)(train.CoefficientTrain ?? 1) * basicPrice;
        }

        private OrderTicketViewModel CreateOrderTicketViewModel(Coach coach, List<Seat> occupiedSeats, string coachCategory, decimal ticketPrice)
        {
            var train = coach.IdTrainNavigation;
            return new OrderTicketViewModel
            {
                Train = train,
                IdTrain = train.IdTrain,
                OccupiedSeats = occupiedSeats,
                PointStart = train.IdTrainRouteNavigation.PointStart,
                PointEnd = train.IdTrainRouteNavigation.PointEnd,
                DateStart = train.DateStart?.ToShortDateString(),
                Price = ticketPrice,
                VehicleType = coachCategory
            };
        }


        [HttpPost]
        public async Task<IActionResult> Index(List<string> listSeats, string Fullname, string Phone, string Email, decimal TotalPrice)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Access");
            }
            if (!ModelState.IsValid)
            {
                return NotFound();          // Trả về view nếu dữ liệu không hợp lệ
            }

            //Lấy id người dùng từ session
            var httpContext = _httpContextAccessor.HttpContext;
            var idcus = httpContext.Session.GetInt32("UserID");

            if (idcus == null)
            {
                return NotFound();
            }
            // Lưu danh sách ghế vào Session khi người dùng đặt vé
            string jsonSeats = JsonConvert.SerializeObject(listSeats);
            HttpContext.Session.SetString("SelectedSeats", jsonSeats);

            HttpContext.Session.SetString("Fullname", Fullname);
            HttpContext.Session.SetString("Phone", Phone);
            HttpContext.Session.SetString("Email", Email);
            HttpContext.Session.SetString("TotalPrice", TotalPrice.ToString());



            // Chuẩn bị dữ liệu cho VNPay
            string idOrder = DateTime.Now.ToString("yyyyMMdd");
            int orderId = Convert.ToInt32(idOrder);
            var vnPayModel = new VnPayRequestModel
            {
                Amount = (double)TotalPrice * 100,
                CreatedDate = DateTime.Now,
                Description = $"Thanh toán",
                Fullname = Fullname,
                OrderId = orderId
            };

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

        public async Task<IActionResult> PaymentCallBack()
        {
            var vnpayResponse = _vnPayService.PaymentExecute(Request.Query);
            if (vnpayResponse.Success)
            {
                // Lấy thông tin từ Session
                var fullname = HttpContext.Session.GetString("Fullname");
                var idcus = HttpContext.Session.GetInt32("UserID");
                var phone = HttpContext.Session.GetString("Phone");
                var email = HttpContext.Session.GetString("Email");
                var totalPrice = decimal.Parse(HttpContext.Session.GetString("TotalPrice"));
                var idCoach = HttpContext.Session.GetInt32("idCoach");

                // Tạo đơn hàng và lưu vào cơ sở dữ liệu
                var order = new Order
                {
                    UnitPrice = (double)totalPrice * 100,
                    DateOrder = DateTime.Now,
                    NameCus = fullname,
                    Phone = phone,
                    IdCus = idcus
                    // Thêm các thuộc tính khác của đơn hàng nếu có
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                // Lấy lại IdOrder vừa lưu
                var orderId = order.IdOrder;

                string jsonSeats = HttpContext.Session.GetString("SelectedSeats");
                List<string> listSeats = JsonConvert.DeserializeObject<List<string>>(jsonSeats);
                string listSeatsFinal = listSeats.First();
                List<string> seats = JsonConvert.DeserializeObject<List<string>>(listSeatsFinal);

                // Lặp qua từng seatName trong danh sách
                foreach (string seatName in seats)
                {
                    // Tìm ghế dựa vào NameSeat và IdCoach
                    var seat = _context.Seats.FirstOrDefault(s => s.NameSeat == seatName && s.IdCoach == idCoach);

                    if (seat != null)
                    {
                        // Cập nhật trạng thái ghế thành đã đặt
                        seat.State = true;

                        var ticket = new Ticket
                        {
                            Date = DateTime.Now,
                            Price = (double)totalPrice,         // Giá vé (có thể khác nhau nếu có giảm giá)
                            IdSeat = seat.IdSeat,               // Lấy Id ghế từ cơ sở dữ liệu
                            IdTrain = (int)_context.Coaches
                                            .Where(c => c.IdCoach == seat.IdCoach)
                                            .Select(c => c.IdTrain)
                                            .FirstOrDefault(),   // Lấy IdTrain từ bảng Coach
                            IdOrder = orderId                   // Gán IdOrder cho vé
                        };
                        _context.Tickets.Add(ticket);
                    }
                }
                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
                return RedirectToAction("ConfirmBooking", new { orderId = orderId });

            }
            //Gửi vé điện tử qua email  
            else
            {
                // Handle payment fail
                return RedirectToAction("PayFail");
            }
        }

        public async Task<IActionResult> ConfirmBooking(int orderId)
        {
            var orders = _context.Orders.Find(orderId);
            if (orders == null)
            {
                return View("Error");
            }

            // Tìm đơn hàng theo orderId
            var order = _context.Orders
            .Include(o => o.Tickets)
                .ThenInclude(t => t.IdSeatNavigation)
                    .ThenInclude(s => s.IdCoachNavigation)
                        .ThenInclude(c => c.IdTrainNavigation)
            .Include(o => o.Tickets)
                .ThenInclude(t => t.IdTrainNavigation)
                    .ThenInclude(tr => tr.IdTrainRouteNavigation)
            .FirstOrDefault(o => o.IdOrder == orderId);



            // Chuyển đổi dữ liệu sang ViewModel
            var tickets = order.Tickets.Select(ticket => new InfoTicketsendtoEmailViewModel
            {
                tenKH = order.NameCus,
                //soXe = ticket.IdTrainNavigation?.NameTrain ?? "Chưa xác định",
                soGhe = ticket.IdSeatNavigation?.NameSeat,
                noiDi = ticket.IdTrainNavigation.IdTrainRouteNavigation.PointStart,
                noiDen = ticket.IdTrainNavigation.IdTrainRouteNavigation.PointEnd,
                ngayKhoiHanh = ticket.IdTrainNavigation?.DateStart ?? DateTime.Now,
                giaVe = ticket.Price
            }).ToList();


            // tạo HTML cho vé 
            string ticketHtml = GenerateTicketHtml(tickets);
            var Email = HttpContext.Session.GetString("Email");

            // gửi  email
            string subject = "Vé xe khách từ Ticket Sales";
            string email = Email;
            await _emailService.SendTicketEmailAsync(email, subject, ticketHtml);

            //gửi thành công
            return RedirectToAction("Pay_Success");
        }

        private string GenerateTicketHtml(List<InfoTicketsendtoEmailViewModel> tickets)
        {
            string templatePath = Path.Combine(_env.WebRootPath, "ContentEmail", "TicketTemplate.html");
            string template = System.IO.File.ReadAllText(templatePath);

            // Tạo nội dung HTML cho các vé
            StringBuilder ticketsHtml = new StringBuilder();
            foreach (var ticket in tickets)
            {
                ticketsHtml.Append("<div class='ticket'>");
                ticketsHtml.Append("<div class='header'><h1>Thông tin vé xe</h1></div>");
                ticketsHtml.Append("<div class='content'>");
                ticketsHtml.Append("<div class='route-title'><span class='label'>Tuyến đường</span></div>");
                ticketsHtml.Append($"<div class='route-info'><span class='route'>{ticket.noiDi} - {ticket.noiDen}</span></div>");
                ticketsHtml.Append($"<div class='passenger'><strong>Người đi:</strong> <span>{ticket.tenKH}</span></div>");
                ticketsHtml.Append($"<div class='datetime'><strong>Ngày & giờ:</strong> <span>{ticket.ngayKhoiHanh.ToString("dd/MM/yyyy HH:mm:ss")}</span></div>");
                ticketsHtml.Append("<div class='seat-bus'>");
                ticketsHtml.Append($"<div class='seat'><strong>Ghế:</strong> <span>{ticket.soGhe}</span></div>");
                ticketsHtml.Append($"<div class='bus-number'><strong>Số xe:</strong> <span>{ticket.soXe}</span></div>");
                ticketsHtml.Append("</div>");
                ticketsHtml.Append($"<div class='price'><strong>Giá:</strong> <span>{ticket.giaVe * 1000}vnđ</span></div>");
                ticketsHtml.Append("</div>");
                ticketsHtml.Append("<div class='footer'><p>Liên hệ: +123456789 | Email: thanhtanPresident@company.com</p>");
                ticketsHtml.Append("<p>Điều khoản và điều kiện áp dụng. Vui lòng đến 15 phút trước khi khởi hành.</p></div>");
                ticketsHtml.Append("</div>");
            }

            // Thay thế nội dung vé vào template
            template = template.Replace("{{Tickets}}", ticketsHtml.ToString());

            return template;
        }



        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserSession") != null;
        }
    }
}
