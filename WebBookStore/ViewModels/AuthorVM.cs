using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class AuthorVM
    {
        public string Name { get; set; }
        public string? Biography { get; set; }
        public string Avatar { get; set; }
    }
}
