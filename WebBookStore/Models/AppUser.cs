using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebBookStore.Models
{
    public class AppUser : IdentityUser
    {
        /*[Key]
        [Required]
        public string Id { get; set; }
        */
        [Required]
        [Display(Name = "Tên người dùng")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên phải từ 5 đến 100 ký tự")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public DateOnly? Birthday { get; set; }
        public int Gender { get; set; }  // 0: Nữ, 1: Nam
    }
}
