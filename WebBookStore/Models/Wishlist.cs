using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebBookStore.Models.Books;

namespace WebBookStore.Models
{
    public class Wishlist
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string BookId { get; set; }
        public virtual Book Book { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
     


    }
}
