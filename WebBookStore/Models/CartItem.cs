using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookStore.Models
{
    [NotMapped]
    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Giá tại thời điểm thêm vào giỏ (tránh thay đổi sau này)
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
