using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
        public class HomePageBooksVM
        {
            public List<Book> AllBooks { get; set; }         // Hiển thị ở Tab 1
            public List<Book> BookOption1 { get; set; }   // Tab 2
            public List<Book> BookOption2 { get; set; }     // Tab 3
            public List<Book> BookOption3 { get; set; }     // Tab 4
            public List<Book> BookOption4 { get; set; }      // Tab 5

    }
}
