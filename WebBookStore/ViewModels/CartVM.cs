using System.ComponentModel.DataAnnotations;
using static WebBookStore.Models.Book;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookStore.ViewModels
{
    public class CartVM
    {
        public string UserName { get; set; }
        public string BookName { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; } // Giá tại thời điểm thêm vào giỏ (tránh thay đổi sau này)

    }
}

