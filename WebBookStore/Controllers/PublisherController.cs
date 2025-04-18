using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;

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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _publisherRepository.AddPublisherAsync(publisher);
                    return RedirectToAction(nameof(Index)); // ← Điều hướng về Index
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi khi thêm nhà xuất bản.");
                }
            }

            return View(publisher); // ← Trả lại view nếu có lỗi
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
        public async Task<ActionResult> Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _publisherRepository.UpdatePublisherAsync(publisher);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi khi cập nhật nhà xuất bản.");
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
        [HttpPost]
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
