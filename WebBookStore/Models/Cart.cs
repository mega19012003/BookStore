using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookStore.Models
{
    [NotMapped]
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }  // Foreign Key to Identity
        public List<CartItem> CartItems { get; set; }
    }
}

