namespace WebBookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}
