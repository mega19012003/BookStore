using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Helpers;
using WebBookStore.Models;
using WebBookStore.Repositories;

namespace WebBookStore.Areas.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly BookDbContext _context;

        public CartController(BookDbContext context)
        {
            _context = context;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(CartKey.CART_KEY) ?? new List<CartItem>();
   
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(Cart);
        }
  
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = Cart;

            var item = cart.FirstOrDefault(i => i.Book.Id == id);
            if (item == null)
            {
                var book = _context.Books.FirstOrDefault(p => p.Id == id);
                if (book == null)
                {
                    return Redirect("/404");
                }

                item = new CartItem
                {
                    Book = book,
                    Quantity = quantity,
                    UnitPrice = book.FinalPrice
                };
                cart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }

            HttpContext.Session.Set(CartKey.CART_KEY, cart); // Cập nhật lại session
            return RedirectToAction("Index");
        }
   
        public IActionResult RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.FirstOrDefault(i => i.Book.Id == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(CartKey.CART_KEY, cart);
            }
            return RedirectToAction("Index");
        }
    }
}
