using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Services;

namespace WebBookStore.Areas.Customer.Controllers
{
    public class PaymentController : Controller
    {

        private IMomoService _momoService;
        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;

        }
        [HttpPost]
        [Route("CreatePaymentUrl")]
        public async Task<IActionResult> CreatePaymentUrl(OrderInfo model)
        {
            var response = await _momoService.CreatePaymentAsync(model);
            return Redirect(response.PayUrl);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
