using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookStore.ViewModels
{
    public class WishlistVM
    {

        public string UserName { get; set; }
        public string BookName { get; set; }
    }
}
