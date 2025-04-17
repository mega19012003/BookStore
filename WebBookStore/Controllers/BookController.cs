using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;

namespace WebBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IPublisherRepository publisherRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _publisherRepository = publisherRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return View(books);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorRepository.GetAllAuthorsAsync();
            ViewBag.Categories = await _categoryRepository.GetAllCategoriesAsync();
            ViewBag.Publishers = await _publisherRepository.GetAllPublishersAsync();
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile coverImage, List<IFormFile> galleryImages)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            if (coverImage != null && coverImage.Length > 0)
            {
                var coverFileName = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);
                var coverPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books", coverFileName);

                using (var stream = new FileStream(coverPath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                book.CoverUrl = "/images/books/" + coverFileName;
            }

            // xử lý nhiều ảnh
            if (galleryImages != null && galleryImages.Any())
            {
                book.Images = new List<Image>();
                foreach (var image in galleryImages)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    book.Images.Add(new Image
                    {
                        ImageUrl = "/images/books/" + fileName
                    });
                }
            }
            await _bookRepository.AddBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            ViewBag.Authors = await _authorRepository.GetAllAuthorsAsync();
            ViewBag.Categories = await _categoryRepository.GetAllCategoriesAsync();
            ViewBag.Publishers = await _publisherRepository.GetAllPublishersAsync();

            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book, IFormFile? newCoverImage, List<IFormFile>? newGalleryImages)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            // Update cover image if changed
            if (newCoverImage != null && newCoverImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(newCoverImage.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newCoverImage.CopyToAsync(stream);
                }

                book.CoverUrl = "/images/books/" + fileName;
            }

            // Add new gallery images (có thể thêm hoặc không)
            if (newGalleryImages != null && newGalleryImages.Any())
            {
                foreach (var image in newGalleryImages)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    book.Images.Add(new Image
                    {
                        ImageUrl = "/images/books/" + fileName
                    });
                }
            }

            await _bookRepository.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookRepository.DeleteBookAsync(id); 
            return RedirectToAction(nameof(Index));
        }
    }
}
