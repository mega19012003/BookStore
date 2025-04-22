using Microsoft.AspNetCore.Mvc;
using WebBookStore.Repositories;

namespace WebBookStore.ViewComponents
{
    public class FeaturedBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public FeaturedBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // Phương thức lấy sách bán chạy
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopSellingBooksAsync(count);
            return View(books);
        }
    }
}
