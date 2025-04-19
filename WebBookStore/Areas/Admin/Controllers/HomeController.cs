using Microsoft.AspNetCore.Mvc;

namespace WebBookStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
