using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class OrderVM
    {
        public string Username { get; set; }
        public string BookName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
