using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;

namespace WebBookStore.Areas.Customer.Controllers
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
    }
}
