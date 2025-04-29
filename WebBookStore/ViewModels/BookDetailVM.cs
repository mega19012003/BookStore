using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class BookDetailVM
    {
        public Book Book { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public double AverageRating { get; set; }
    }
}
