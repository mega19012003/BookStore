using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
