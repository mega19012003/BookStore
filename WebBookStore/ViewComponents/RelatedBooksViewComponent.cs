using Microsoft.AspNetCore.Mvc;
using WebBookStore.Repositories;

namespace WebBookStore.ViewComponents
{
    public class RelatedBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public RelatedBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId, int currentBookId, int count)
        {
            var relatedBooks = await _bookRepository.GetBooksByCategoryAsync(categoryId);

            var randomRelated = relatedBooks
                .Where(b => b.Id != currentBookId)
                .OrderBy(_ => Guid.NewGuid())
                .Take(count)      // Dùng biến count ở đây
                .ToList();

            return View(randomRelated);
        }
    }
}