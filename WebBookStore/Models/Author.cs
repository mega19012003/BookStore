﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models
{
    public class Author
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên tác giả")]
        [StringLength(100, ErrorMessage = "Tên tác giả không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Display(Name = "Tiểu sử")]
        [StringLength(500, ErrorMessage = "Tiểu sử không được vượt quá 500 ký tự.")]
        public string? Biography { get; set; }

        [DisplayName("Hình ảnh")]
        public string AvatarUrl { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public bool IsDeleted { get; set; } = false; //Soft Delete
    }
}
