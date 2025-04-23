using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Models;
using WebBookStore.Repositories;

namespace WebBookStore.Controllers
{
    public class CartController : Controller
    {

        private readonly IBookRepository _bookRepository;

        public CartController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> AddToCart(int bookId)
        {
            var cart = GetCartFromSession();

            var book = await _bookRepository.GetBookByIdAsync(bookId); // dùng await
            if (book != null)
            {
                var existing = cart.FirstOrDefault(i => i.Book.Id == bookId);
                if (existing != null)
                {
                    existing.Quantity += 1;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        Book = book,
                        Quantity = 1,
                        UnitPrice = book.FinalPrice 
                    });
                }

                SaveCartToSession(cart);
            }

            return RedirectToAction("Index", "Cart");
        }

        private List<CartItem> GetCartFromSession()
        {
            var session = HttpContext.Session.GetString("Cart");
            return session != null
                ? JsonSerializer.Deserialize<List<CartItem>>(session)
                : new List<CartItem>();
        }

        private void SaveCartToSession(List<CartItem> cart)
        {
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }

        // GET: CartController
        public ActionResult Index()
        {
            return View();
        }
    }
}
