using Microsoft.AspNetCore.Mvc;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;

namespace WebBookStore.ViewComponents
{
    public class CustomBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public CustomBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allBooks = await _bookRepository.GetNewestBooksAsync(12);

            var vm = new HomePageBooksVM
            {
                AllBooks = allBooks,
                AdventureBooks = allBooks.Where(b => b.Category.Name == "Tiểu thuyết").ToList(),
                FantasyBooks = allBooks.Where(b => b.Category.Name == "Giả tưởng").ToList(),
                RomanceBooks = allBooks.Where(b => b.Category.Name == "Trinh thám").ToList(),
                HorrorBooks = allBooks.Where(b => b.Category.Name == "Kinh dị").ToList(),
            };

            return View(vm); // đúng với default.cshtml của bạn
        }
    }
}
