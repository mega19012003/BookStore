using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;
using WebBookStore.ViewModels;

namespace WebBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        private readonly BookDbContext _dbcontext;

        public DashBoardController(BookDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            var now = DateTime.Now;
            var startOfWeek = now.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);

            var yearlyRevenue = _dbcontext.Bills
                .Where(o => o.OrderDate.Year == now.Year)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            var monthlyRevenue = _dbcontext.Orders
                .Where(o => o.OrderDate.Month == now.Month && o.OrderDate.Year == now.Year)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            var weeklyRevenue = _dbcontext.Orders
                .Where(o => o.OrderDate >= startOfWeek && o.OrderDate <= now)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            var pendingOrders = _dbcontext.Bills
                .Count(o => o.Status == "pending");

            var viewModel = new DashboardVM
            {
                YearlyRevenue = yearlyRevenue,
                MonthlyRevenue = monthlyRevenue,
                WeeklyRevenue = weeklyRevenue,
                PendingOrders = pendingOrders
            };

            return View(viewModel);
        }
        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
        public IActionResult GetMonthlySalesData(int? year)
        {
            int selectedYear = year ?? DateTime.Now.Year;

            var salesData = Enumerable.Range(1, 12)
                .Select(month => new
                {
                    Month = month,
                    TotalSold = _dbcontext.BillDetails
                        .Where(p => p.Bill.OrderDate.Year == selectedYear && p.Bill.OrderDate.Month == month)
                        .Sum(p => (int?)p.Quantity) ?? 0
                })
                .ToList();

            return Json(salesData);
        }

        public IActionResult GetTopCategories()
        {
            var data = _dbcontext.BillDetails
                .Include(p => p.Book).ThenInclude(p => p.Category)
                .GroupBy(p => p.Book.Category.Name)
                .Select(g => new {
                    CategoryName = g.Key,
                    TotalSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToList();

            return Json(data);
        }
    }
}
