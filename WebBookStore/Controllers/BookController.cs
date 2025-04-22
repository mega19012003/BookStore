using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;

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
            /* var books = await _bookRepository.GetAllBooksAsync();
            return View(books.Where(b => !b.IsDeleted));*/
        }

        /*// GET: Book/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var authors = await _authorRepository.GetAllAuthorsAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");

            var publishers = await _publisherRepository.GetAllPublishersAsync();
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");

            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ViewBag.ImageError = "Vui lòng chọn ảnh bìa.";
                return View(book);
            }

            if (string.IsNullOrEmpty(book.ProductCode))
            {
                book.ProductCode = GenerateProductCode();
            }

            // Tạo thư mục lưu ảnh nếu chưa có
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file duy nhất
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu ảnh vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            book.CoverUrl = "/images/books/" + uniqueFileName;

            // Thêm vào database
            await _bookRepository.AddBookAsync(book);
            return RedirectToAction(nameof(Index));
        }


        // Hàm tạo mã sản phẩm tự động
        private string GenerateProductCode()
        {
            return "P" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
        */

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }
        /*
        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var authors = await _authorRepository.GetAllAuthorsAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");

            var publishers = await _publisherRepository.GetAllPublishersAsync();
            ViewBag.Publishers = new SelectList(publishers, "Id", "Name");

            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile image, [Bind("Id,Title,Price,DiscountPrice,PublishYear,Description,AuthorId,CategoryId,PublisherId,OutOfStock")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            var bookToUpdate = await _bookRepository.GetBookByIdAsync(id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            // Cập nhật các thông tin khác của book (trừ CoverUrl)
            bookToUpdate.Title = book.Title;
            bookToUpdate.Price = book.Price;
            bookToUpdate.DiscountPrice = book.DiscountPrice;
            bookToUpdate.PublishYear = book.PublishYear;
            bookToUpdate.Description = book.Description;
            bookToUpdate.AuthorId = book.AuthorId;
            bookToUpdate.CategoryId = book.CategoryId;
            bookToUpdate.PublisherId = book.PublisherId;
            bookToUpdate.OutOfStock = book.OutOfStock;

            // Kiểm tra nếu có ảnh mới
            if (image != null && image.Length > 0)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(bookToUpdate.CoverUrl))
                {
                    var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, bookToUpdate.CoverUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                // Lưu ảnh mới
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books");
                Directory.CreateDirectory(uploads); // Tạo thư mục nếu chưa có

                var fullPath = Path.Combine(uploads, fileName);
                using (var fs = new FileStream(fullPath, FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                }

                // Cập nhật CoverUrl trong book
                bookToUpdate.CoverUrl = "/images/books/" + fileName;
            }

            try
            {
                await _bookRepository.UpdateBookAsync(bookToUpdate);
                return RedirectToAction(nameof(Index)); // Quay lại trang danh sách sách
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            // Nếu đến đây, trả về lại view để chỉnh sửa
            ViewBag.Authors = new SelectList(await _authorRepository.GetAllAuthorsAsync(), "Id", "Name", bookToUpdate.AuthorId);
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name", bookToUpdate.CategoryId);
            ViewBag.Publishers = new SelectList(await _publisherRepository.GetAllPublishersAsync(), "Id", "Name", bookToUpdate.PublisherId);

            return View(bookToUpdate); // Trả về view để chỉnh sửa nếu có lỗi
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
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book != null)
            {
                book.IsDeleted = true;
                await _bookRepository.UpdateBookAsync(book);
            }
            return RedirectToAction(nameof(Index));
        }*/
    }
}
