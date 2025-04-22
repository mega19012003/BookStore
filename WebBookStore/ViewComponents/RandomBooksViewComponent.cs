using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;

namespace WebBookStore.ViewComponents
{
    public class RandomBooks : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public RandomBooks(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // Phương thức lấy sách ngẫu nhiên
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetRandomBooksAsync(count)
            ?? new List<Book>();
            return View(books);
        }
    }
}

