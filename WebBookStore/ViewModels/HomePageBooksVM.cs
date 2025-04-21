using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
        public class HomePageBooksVM
        {
            public List<Book> AllBooks { get; set; }         // Hiển thị ở Tab 1
            public List<Book> AdventureBooks { get; set; }   // Tab 2
            public List<Book> FantasyBooks { get; set; }     // Tab 3
            public List<Book> RomanceBooks { get; set; }     // Tab 4
            public List<Book> HorrorBooks { get; set; }      // Tab 5

    }
}
