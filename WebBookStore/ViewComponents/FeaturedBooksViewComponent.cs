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
        public async Task<IViewComponentResult> InvokeAsync(int authorId, int currentBookId, int count)
        {
            var featuredBooks = await _bookRepository.GetBooksByAuthorAsync(authorId);

            var randomFeatured = featuredBooks
                .Where(b => b.Id != currentBookId)
                .OrderBy(_ => Guid.NewGuid())
                .Take(count)      
                .ToList();

            return View(randomFeatured);
        }
    }
}
