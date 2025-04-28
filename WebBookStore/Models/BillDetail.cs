using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models
{

    public class BillDetail
    {
        [Key]
        public int BillDetailId { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
