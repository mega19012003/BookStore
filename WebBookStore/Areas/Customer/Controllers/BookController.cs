using System;
using System.Drawing.Printing;
using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebBookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IReviewRepository  _reviewRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IPublisherRepository publisherRepository, IReviewRepository reviewRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _publisherRepository = publisherRepository;
            _reviewRepository = reviewRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return View(books);
            /* var books = await _bookRepository.GetAllBooksAsync();
            return View(books.Where(b => !b.IsDeleted));*/
        }

        [ActionName("AllBooksWithPages")]
        public async Task<IActionResult> GetAllBooksWithPagesAsync(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var allBooks = await _bookRepository.GetAllBooksAsync();

            var books = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalBooks = allBooks.Count();
            int totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View("AllBooksWithPages", books);
        }

        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }

            var books = await _bookRepository.GetAllBooksAsync();
            var results = books.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            return View("SearchResults", results);
        }

        public async Task<IActionResult> GetBooksByCategory(int categoryId, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var allBooks = await _bookRepository.GetBooksByCategoryAsync(categoryId);

            // Đếm tổng số sách
            int totalBooks = allBooks.Count();

            // Lấy trang hiện tại
            var books = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
            .ToList();

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            ViewBag.CategoryName = category?.Name ?? "Không rõ thể loại";

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
            ViewBag.CategoryId = categoryId;
            return View("BooksByCategory", books); 
        }

        public async Task<IActionResult> GetBooksByPublisher(int publisherId, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var allBooks = await _bookRepository.GetBooksByPublisherAsync(publisherId);

            // Đếm tổng số sách
            int totalBooks = allBooks.Count();

            // Lấy trang hiện tại
            var books = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var publisher = await _publisherRepository.GetPublisherByIdAsync(publisherId);
            ViewBag.PublisherName = publisher?.Name ?? "Không rõ nhà xuất bản";

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
            ViewBag.PublisherId = publisherId;

            return View("BooksByPublisher", books);
        }

        public async Task<IActionResult> GetBooksByAuthor(int authorId, int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var allBooks = await _bookRepository.GetBooksByAuthorAsync(authorId);

            // Đếm tổng số sách
            int totalBooks = allBooks.Count();

            // Lấy trang hiện tại
            var books = allBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var author = await _authorRepository.GetAuthorByIdAsync(authorId);
            ViewBag.AuthorName = author?.Name ?? "Không rõ tác giả";

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
            ViewBag.AuthorId = authorId;

            return View("BooksByAuthor", books); 
        }
        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            var reviews = await _reviewRepository.GetReviewsByBookIdAsync(id);
            var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            if (book == null)
            {
                return NotFound();
            }
            // Tạo view model với sách và reviews
            var viewModel = new BookDetailVM
            {
                Book = book,
                Reviews = reviews,
                AverageRating = averageRating
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(ReviewVM model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Detail", new { id = model.BookId });

            var review = new Review
            {
                BookId = model.BookId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.Now
            };

            await _reviewRepository.AddReviewAsync(review);
            return RedirectToAction("Details", new { id = model.BookId });
        }

    }
}
