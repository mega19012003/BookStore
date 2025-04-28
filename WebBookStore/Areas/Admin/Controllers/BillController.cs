using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Repositories;

namespace WebBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BillController : Controller
    {
        private readonly IBillRepository _billRepository;

        public BillController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var bill = await _billRepository.GetAllBillsAsync();
            return View(bill);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            // Xóa đơn hàng
            await _billRepository.DeleteBillAsync(bill);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptOrder(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill != null)
            {
                bill.Status = "Đã giao hàng";
                await _billRepository.UpdateBillAsync(bill);
                return RedirectToAction("Index");  
            }

            return NotFound();
        }
    }
}
