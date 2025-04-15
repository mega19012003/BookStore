using System.ComponentModel.DataAnnotations;
using static WebBookStore.Models.Book;

namespace WebBookStore.ViewModels
{
    public class ImageVM
    {
        public string ImageUrl { get; set; }  
        public string BookName { get; set; }  
        public int DisplayOrder { get; set; }  // Thứ tự hiển thị ảnh (nếu có)

    }
}
