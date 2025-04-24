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
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private const string CartKey = "MYCART";
        private readonly BookDbContext _context;

        public CartController(BookDbContext context)
        {
            _context = context;
        }

        private List<CartItem> Cart
        {
            get
            {
                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartKey);
                return cartItems ?? new List<CartItem>();
            }
            set
            {
                HttpContext.Session.SetObjectAsJson(CartKey, value);
            }
        }

        public IActionResult Index()
        {
            var cartItems = Cart;
            return View(cartItems);
        }

        public IActionResult AddToCart(int id, int quantity)
        {
            var cartItems = Cart;
            var existingItem = cartItems.FirstOrDefault(item => item.Book.Id == id);

            if (existingItem == null)
            {
                var book = _context.Books.Find(id);
                if (book == null)
                {
                    return NotFound();
                }

                var newItem = new CartItem
                {
                    Book = book,
                    Quantity = quantity,
                    UnitPrice = book.FinalPrice
                };

                cartItems.Add(newItem);
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            Cart = cartItems;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = Cart;
            var item = cart.FirstOrDefault(x => x.Book.Id == id);

            if (item == null)
                return Json(new { success = false });

            item.Quantity = quantity;
            Cart = cart;  // Lưu giỏ hàng vào session

            var newTotal = item.Quantity * item.UnitPrice;

            return Json(new { success = true, newTotal = newTotal.ToString("N0") });
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cartItems = Cart;
            var existingItem = cartItems.FirstOrDefault(item => item.Book.Id == id);

            if (existingItem != null)
            {
                cartItems.Remove(existingItem);
                Cart = cartItems;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
