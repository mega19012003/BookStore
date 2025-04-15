using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên thể loại")]
        [StringLength(100, ErrorMessage = "Tên thể loại không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string? Description { get; set; }
    }
}
