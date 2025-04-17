using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;

namespace WebBookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PublisherController(IPublisherRepository publisherRepository, IWebHostEnvironment webHostEnvironment)
        {
            _publisherRepository = publisherRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PublisherController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var publisher = await _publisherRepository.GetAllPublishersAsync();
            return View(publisher);
        }

        // GET: PublisherController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // GET: PublisherController/Create
        [HttpPost]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Publisher publisher, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "publishers", fileName);

                        // Lưu ảnh vào thư mục
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        // Cập nhật đường dẫn ảnh trong đối tượng Publisher
                        publisher.CoverUrl = "/images/publishers/" + fileName;
                    }

                    await _publisherRepository.AddPublisherAsync(publisher);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi thêm nhà xuất bản.");
                    return View(publisher);
                }
            }
            return View(publisher);
        }

        // GET: PublisherController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: PublisherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Publisher publisher, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        // Tạo tên file duy nhất cho ảnh
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "publishers", fileName);

                        // Lưu ảnh vào thư mục
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        // Cập nhật đường dẫn ảnh trong đối tượng Publisher
                        publisher.CoverUrl = "/images/Publishers/" + fileName;
                    }

                    await _publisherRepository.UpdatePublisherAsync(publisher);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi cập nhật nhà xuất bản.");
                    return View(publisher);
                }
            }
            return View(publisher);
        }

        // GET: PublisherController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: PublisherController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _publisherRepository.DeletePublisherAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi khi xóa nhà xuất bản.");
                return View();
            }
        }
    }
}
