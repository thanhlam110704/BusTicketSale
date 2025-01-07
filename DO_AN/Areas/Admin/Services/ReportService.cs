using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DO_AN.Models;

public class ReportService
{
    private readonly DOAN_BoSungContext _context;

    public ReportService(DOAN_BoSungContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetMonthlyRevenue(int year, int month)
    {
        var orders = await _context.Orders
            .Where(o => o.DateOrder != null && o.DateOrder.Value.Year == year && o.DateOrder.Value.Month == month)
            .ToListAsync();

        decimal totalRevenue = (decimal)orders.Sum(o => o.UnitPrice);   // Example calculation

        return totalRevenue;
    }


    public async Task<int> GetMostTravelledRouteByDay(DateTime date)
    {
        var ticketCounts = await _context.Tickets
            .Where(t => t.Date.HasValue && t.Date.Value.Date == date.Date)
            .GroupBy(t => t.IdTrainNavigation.IdTrainRouteNavigation.PointStart + " - " + t.IdTrainNavigation.IdTrainRouteNavigation.PointEnd)
            .Select(g => new { TrainRouteId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .FirstOrDefaultAsync();

        if (ticketCounts != null)
        {
            return int.Parse(ticketCounts.TrainRouteId);
        }

        return 0; // Handle no data case
    }

}
