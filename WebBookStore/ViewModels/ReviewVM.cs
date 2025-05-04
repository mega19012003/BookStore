using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string UserName { get; set; } 

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
