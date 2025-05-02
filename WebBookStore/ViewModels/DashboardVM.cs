using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class DashboardVM
    {
        public decimal YearlyRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal WeeklyRevenue { get; set; }
        public int PendingOrders { get; set; }
        public List<Review> LatestReviews { get; set; }
        public List<Bill> LatestOrders { get; set; }
    }
}
