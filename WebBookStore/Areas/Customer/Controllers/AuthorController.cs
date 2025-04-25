using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;

namespace WebBookStore.Areas.Customer.Controllers
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
    }
}
