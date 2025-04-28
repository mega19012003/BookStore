using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class CheckoutVM
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }

        public string Note { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } // "CashOnDelivery" hoặc "VNPay"
    }
}
