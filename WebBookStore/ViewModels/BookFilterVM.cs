using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class BookFilterVM
    {
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }

        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }

        public List<Book> Books { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
