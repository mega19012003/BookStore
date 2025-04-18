using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;

namespace WebBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(IAuthorRepository authorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _authorRepository = authorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: AuthorController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var author = await _authorRepository.GetAllAuthorsAsync();
            return View(author);
            /*var authors = await _authorRepository.GetAllAuthorsAsync();
            var activeAuthors = authors.Where(a => !a.IsDeleted).ToList(); // Lọc các tác giả chưa bị xóa mềm

            return View(activeAuthors);*/
        }

        // GET: AuthorController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // GET: Author/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ViewBag.ImageError = "Vui lòng chọn hình ảnh.";
                return View(author);
            }

            // Tạo thư mục lưu ảnh nếu chưa có
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/authors");
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

            // Gán đường dẫn ảnh cho Author (để hiển thị sau này)
            author.AvatarUrl = "/images/authors/" + uniqueFileName;

            // Thêm vào database
            await _authorRepository.AddAuthorAsync(author);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile image)
        {
            // 1) Lấy bản ghi gốc
            var authorToUpdate = await _authorRepository.GetAuthorByIdAsync(id);
            if (authorToUpdate == null) return NotFound();

            // 2) Cập nhật các field text (Name, Biography) từ form
            if (await TryUpdateModelAsync(authorToUpdate, "",
                    a => a.Name,
                    a => a.Biography))
            {
                // 3) Nếu có ảnh mới thì xử lý upload
                if (image != null && image.Length > 0)
                {
                    // Xóa ảnh cũ
                    if (!string.IsNullOrEmpty(authorToUpdate.AvatarUrl))
                    {
                        var oldPath = Path.Combine(_webHostEnvironment.WebRootPath,
                            authorToUpdate.AvatarUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    // Lưu ảnh mới
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images", "authors");
                    Directory.CreateDirectory(uploads); // nếu chưa tồn tại
                    var fullPath = Path.Combine(uploads, fileName);
                    using (var fs = new FileStream(fullPath, FileMode.Create))
                        await image.CopyToAsync(fs);

                    authorToUpdate.AvatarUrl = "/images/authors/" + fileName;
                }

                // 4) Lưu thay đổi
                await _authorRepository.UpdateAuthorAsync(authorToUpdate);
                return RedirectToAction(nameof(Index));
            }

            // Nếu model binding thất bại, trả lại view với authorToUpdate để giữ nguyên dữ liệu
            return View(authorToUpdate);
        }


        // GET
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return View(author); // sẽ load Views/Author/Delete.cshtml
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _authorRepository.DeleteAuthorAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Có lỗi khi xóa tác giả.");
                var author = await _authorRepository.GetAuthorByIdAsync(id);
                return View(author);
            }
        }
    }
}
