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
            // Lấy 10 nhận xét mới nhất
            var latestReviews = _dbcontext.Reviews
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt)
                .Include(r => r.User) // Load thông tin user
                .Take(10)
                .ToList();

            // Lấy 10 đơn hàng mới nhất
            var latestOrders = _dbcontext.Bills
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToList();
            // Lấy doanh thu trong năm
            var yearlyRevenue = _dbcontext.Bills
                .Where(o => o.OrderDate.Year == now.Year)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            // Lấy doanh thu trong tháng
            var monthlyRevenue = _dbcontext.Bills
                .Where(o => o.OrderDate.Month == now.Month && o.OrderDate.Year == now.Year)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            // Lấy doanh thu trong tuần
            var weeklyRevenue = _dbcontext.Bills
                .Where(o => o.OrderDate >= startOfWeek && o.OrderDate <= now)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            // Lấy số lượng đơn hàng đang chờ xử lý
            var pendingOrders = _dbcontext.Bills
                .Count(o => o.Status == "Pending");

            // Lấy doanh thu theo từng tháng trong năm (12 tháng)
            var monthlySales = Enumerable.Range(1, 12)
                .Select(m => new {
                    Month = m,
                    Total = _dbcontext.Bills
                        .Where(b => b.OrderDate.Year == now.Year && b.OrderDate.Month == m)
                        .Sum(b => (decimal?)b.TotalAmount) ?? 0
                })
                .ToList();

            // Truyền dữ liệu vào ViewBag
            ViewBag.MonthLabels = monthlySales.Select(x => $"Tháng {x.Month}").ToArray();
            ViewBag.MonthValues = monthlySales.Select(x => x.Total).ToArray();

            // Truyền các giá trị khác vào ViewModel
            var viewModel = new DashboardVM
            {
                YearlyRevenue = yearlyRevenue,
                MonthlyRevenue = monthlyRevenue,
                WeeklyRevenue = weeklyRevenue,
                PendingOrders = pendingOrders,
                LatestReviews = latestReviews,
                LatestOrders = latestOrders
            };

            return View(viewModel);
        }
        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMonthlySales()
        {
            var salesData = await _dbcontext.Bills
                .Where(b => b.OrderDate >= new DateTime(DateTime.Now.Year, 1, 1))  
                .GroupBy(b => new { b.OrderDate.Year, b.OrderDate.Month }) 
                .Select(g => new
                {
                    YearMonth = $"{g.Key.Month}/{g.Key.Year}",   
                    TotalSales = g.Sum(b => b.TotalAmount)    
                })
                .OrderBy(s => s.YearMonth)
                .ToListAsync();

            return Ok(salesData);
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
