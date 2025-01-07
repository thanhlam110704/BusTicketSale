using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DO_AN.Models;
using DO_AN.Areas.Admin.ViewModels;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using PagedList.Core;
namespace DO_AN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly DOAN_BoSungContext _context;

        public OrdersController(DOAN_BoSungContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListOrders(int page = 1)
        {
            int pageSize = 10; // Số lượng bản ghi mỗi trang
            var orders = await _context.Orders
                .Select(o => new OrderDetailsVM
                {
                    IdOrder = o.IdOrder,
                    UnitPrice = o.UnitPrice,
                    Date = o.DateOrder,
                    NameCus = o.NameCus,
                    Phone = o.Phone,
                    IdCus = o.IdCus,
                    Tickets = o.Tickets.Select(t => new TicketViewModel
                    {
                        IdTicket = t.IdTicket,
                        DateStart = t.Date,
                        Price = t.Price,
                        SeatName = t.IdSeatNavigation != null ? t.IdSeatNavigation.NameSeat : "Không xác định",
                        PointStart = t.IdTrainNavigation != null ? t.IdTrainNavigation.IdTrainRouteNavigation.PointStart : "Không xác định",
                        PointEnd = t.IdTrainNavigation != null ? t.IdTrainNavigation.IdTrainRouteNavigation.PointEnd : "Không xác định",
                        CoefficientTrain = t.IdTrainNavigation.CoefficientTrain,
                        NameTrain = t.IdTrainNavigation.NameTrain,
                        IdTrain = t.IdTrain.GetValueOrDefault()
                    }).ToList()
                }).ToListAsync();

            // Phân trang
            var paginatedOrders = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Tạo ViewModel cho phân trang
            var viewModel = new PaginatedOrdersViewModel
            {
                Orders = paginatedOrders,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(orders.Count() / (double)pageSize)
            };

            return View(viewModel);

         
        }

        public IActionResult ExportOrdersToCsv()
        {
            var orders = _context.Orders
                .Include(o => o.Tickets)
                    .ThenInclude(t => t.IdTrainNavigation)
                        .ThenInclude(tr => tr.IdTrainRouteNavigation)
                .Include(o => o.Tickets)
                    .ThenInclude(t => t.IdSeatNavigation)
                .ToList();

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Encoding = System.Text.Encoding.UTF8,
                NewLine = Environment.NewLine,
            };

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.WriteField("Ma Don Hang");
                csv.WriteField("Ma Ve");
                csv.WriteField("Ngay Dat Ve");
                csv.WriteField("Tong Tien");
                csv.WriteField("Ten KH");
                csv.WriteField("So Dien Thoai");
                csv.WriteField("MaKH");
                csv.WriteField("Ngay Khoi Hanh");
                csv.WriteField("Gia Ve");
                csv.WriteField("Ten Ghe");
                csv.WriteField("Noi Di");
                csv.WriteField("Noi Den");
                csv.WriteField("He So Gia Cua Chuyen");
                csv.WriteField("Ten Chuyen");
                csv.WriteField("Ma Chuyen");
                csv.NextRecord();

                foreach (var order in orders)
                {
                    foreach (var ticket in order.Tickets)
                    {
                        var csvModel = new ExportOrdersToCSVViewModel
                        {
                            IdOrder = order.IdOrder,
                            IdTicket = ticket.IdTicket,
                            BookingDate = ticket.Date,
                            UnitPrice = order.UnitPrice,
                            NameCus = order.NameCus,
                            Phone = order.Phone,
                            IdCus = order.IdCus,
                            DateStart = ticket.IdTrainNavigation?.DateStart,
                            Price = ticket.Price,
                            SeatName = ticket.IdSeatNavigation?.NameSeat,
                            PointStart = ticket.IdTrainNavigation?.IdTrainRouteNavigation?.PointStart,
                            PointEnd = ticket.IdTrainNavigation?.IdTrainRouteNavigation?.PointEnd,
                            CoefficientTrain = ticket.IdTrainNavigation?.CoefficientTrain,
                            NameTrain = ticket.IdTrainNavigation?.NameTrain,
                            IdTrain = ticket.IdTrain ?? 0
                        };
                        csv.WriteRecord(csvModel);
                        csv.NextRecord();
                    }
                }
                writer.Flush();
                var result = memoryStream.ToArray();
                return File(result, "application/octet-stream", "ListOrders.csv");
            }
        }
        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.IdOrder == id)).GetValueOrDefault();
        }
      
    }
}
