using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;
using static WebBookStore.ViewModels.HomePageBooksVM;

namespace WebBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;

        public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            /*var books = await _bookRepository.GetNewestBooksAsync(12);
            if (books == null || !books.Any())
            {
                // Log thêm thông tin để kiểm tra
                _logger.LogWarning("Không có sách mới trong database.");
                return View("NoBooks");
            }
            // Truyền sách vào View
            return View(books);  // Truyền trực tiếp Model vào View
            //return View();*/
            var allBooks = await _bookRepository.GetNewestBooksAsync(12);

            var vm = new HomePageBooksVM
            {
                AllBooks = allBooks,
                AdventureBooks = allBooks.Where(b => b.Category.Name == "Tiểu thuyết").ToList(),
                FantasyBooks = allBooks.Where(b => b.Category.Name == "Giả tưởng").ToList(),
                RomanceBooks = allBooks.Where(b => b.Category.Name == "Trinh thám").ToList(),
                HorrorBooks = allBooks.Where(b => b.Category.Name == "Kinh dị").ToList(),
            };

            return View(vm);
        }

        public async Task<IActionResult> NewAdventureBook()
        {
            int adventureCategoryId = 3; // hoặc lấy từ DB/enum tùy bạn

            var books = await _bookRepository.GetNewestBooksByCategoryAsync(adventureCategoryId, 3);
            if (books == null || !books.Any())
            {
                _logger.LogWarning("Không có sách thể loại Adventure.");
                return View("NoBooks");
            }

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
