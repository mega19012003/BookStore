using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class LoginVM
    {

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
