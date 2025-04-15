using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookStore.Models
{
    public class Wishlist
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
