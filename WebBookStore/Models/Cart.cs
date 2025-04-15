using System.ComponentModel.DataAnnotations;
using static WebBookStore.Models.Books;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Số lượng phải từ 1 đến 100")]
        public int Quantity { get; set; } = 1;

        [Required]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; } // Giá tại thời điểm thêm vào giỏ (tránh thay đổi sau này)

        public DateTime AddedAt { get; set; } = DateTime.Now; 

    }
}
}
