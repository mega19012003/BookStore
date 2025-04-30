using Microsoft.AspNetCore.Mvc.Rendering;
using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class BookFilterVM
    {
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }

        public SelectList Authors { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Publishers { get; set; }

        public List<Book> Books { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
