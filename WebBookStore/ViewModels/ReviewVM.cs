using static WebBookStore.Models.Book;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class ReviewVM
    {
        public string UserName{ get; set; }
        public string BookName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
    }
}

