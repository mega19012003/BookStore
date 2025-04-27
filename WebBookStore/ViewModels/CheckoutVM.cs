using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class CheckoutVM
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public decimal ShippingFee { get; set; }
        public string PaymentMethod { get; set; }
    }
}
