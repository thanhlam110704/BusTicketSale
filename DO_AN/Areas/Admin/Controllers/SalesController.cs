using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DO_AN.Models;
using Newtonsoft.Json;

namespace DO_AN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        private readonly DOAN_BoSungContext _context;

        public SalesController(DOAN_BoSungContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Admin/Sales/DailyRevenueChart")]
        public async Task<IActionResult> DailyRevenueChart(int year, int month)
        {
            if (year == 0 || month == 0)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }
            // Validate year and month input (optional, based on your requirements)
            if (year < 2000 || year > DateTime.Today.Year || month < 1 || month > 12)
            {
                // Handle invalid input
                return BadRequest("Invalid year or month.");
            }

            // Lấy số ngày trong tháng
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // Tính toán doanh thu theo từng ngày trong tháng
            var dailyRevenueData = _context.Orders
                        .Where(o => o.DateOrder.HasValue && o.DateOrder.Value.Year == year && o.DateOrder.Value.Month == month)
                        .GroupBy(o => new
                        {
                            Day = o.DateOrder.Value.Day
                        })
                        .Select(g => new
                        {
                            Day = g.Key.Day,
                            TotalRevenue = g.Sum(o => o.UnitPrice)
                        })
                        .OrderBy(g => g.Day)
                        .ToList();

            // Chuẩn bị dữ liệu cho biểu đồ (theo ngày)
            string[] labels = new string[daysInMonth];
            int[] revenueValues = new int[daysInMonth];

            for (int day = 1; day <= daysInMonth; day++)
            {
                labels[day - 1] = day.ToString();
                var revenue = dailyRevenueData.FirstOrDefault(d => d.Day == day);
                revenueValues[day - 1] = (int)(revenue?.TotalRevenue ?? 0); // Đảm bảo giá trị không null và chuyển về kiểu int
            }

            var chartData = new
            {
                Labels = labels,
                RevenueValues = revenueValues
            };
            ViewData["DailyChartData"] = JsonConvert.SerializeObject(chartData);
            var date = new
            {
                Month = month,
                Year = year
            };
            return View(date);
        }


        public IActionResult AllRoutesWithTripCounts()
        {
            var allRoutes = _context.TrainRoutes
                .Select(route => new
                {
                    Route = route.PointStart + " - " + route.PointEnd,
                    RouteId = route.IdTrainRoute,
                    TicketsCount = _context.Tickets
                        .Count(ticket => ticket.IdTrainNavigation.IdTrainRoute == route.IdTrainRoute)
                })
                .OrderByDescending(r => r.TicketsCount)
                .ToList();

            ViewData["AllRoutes"] = allRoutes;

            return View();
        }
    }
}