using System.ComponentModel.DataAnnotations;
using static WebBookStore.Models.Books;

namespace WebBookStore.Models
{
    public class Image
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }  

        public int BookId { get; set; }  
        public Book Book { get; set; }  

        public int DisplayOrder { get; set; }  // Thứ tự hiển thị ảnh (nếu có)

    }
}
