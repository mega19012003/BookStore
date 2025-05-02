using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;

namespace WebBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var review = await _reviewRepository.GetAllReviewsAsync();
            return View(review);
        }

        // GET: CategoryController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _reviewRepository.DeleteReviewAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Xử lý lỗi khi xóa
                ModelState.AddModelError("", "Có lỗi khi xóa thể loại.");
                return View();
            }
        }
    }
}
