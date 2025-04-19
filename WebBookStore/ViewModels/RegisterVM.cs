using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Tên người dùng")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên phải từ 5 đến 100 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateOnly Birthday { get; set; }

        [Required]
        [Display(Name = "Giới tính")]
        public int Gender { get; set; } // 0: Nữ, 1: Nam
    }
}
