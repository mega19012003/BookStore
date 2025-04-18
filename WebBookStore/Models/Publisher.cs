using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models
{
    public class Publisher
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên nhà xuất bản")]
        [StringLength(100, ErrorMessage = "Tên nhà xuất bản không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        /*[Required]
        [DisplayName("Hình ảnh")]
        [StringLength(1024)]
        public string? CoverUrl { get; set; }*/

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public bool IsDeleted { get; set; } = false; //Soft Delete
    }
}
