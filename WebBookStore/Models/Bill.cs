using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        /*[EmailAddress]
        public string Email { get; set; }*/

        public string Note { get; set; } 

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime? DateDeliver { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }  // VNPay / CashOnDelivery / etc.
        [Required]
        public string Status { get; set; } = "Pending"; // Trạng thái đơn hàng (Pending, Processing, Completed, Canceled)
    }
}
