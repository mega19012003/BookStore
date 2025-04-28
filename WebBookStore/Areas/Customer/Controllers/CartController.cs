using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Helpers;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;

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

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = Cart; 
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Map dữ liệu ViewModel => Model
            var bill = new Bill
            {
                FullName = vm.FullName,
                Address = vm.Address,
                PhoneNumber = vm.PhoneNumber,
                // Email = vm.Email,
                Note = vm.Note,
                TotalAmount = vm.TotalAmount,
                PaymentMethod = vm.PaymentMethod,
                Status = "Pending",
                OrderDate = DateTime.Now,
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            foreach (var cartItem in Cart)
            {
                var product = await _context.Books.FirstOrDefaultAsync(p => p.Id == cartItem.Book.Id);
                if (product != null)
                {
                    product.Quantity -= cartItem.Quantity;  // Giảm số lượng trong cơ sở dữ liệu
                    _context.Books.Update(product);
                }
            }
            await _context.SaveChangesAsync();

            Cart = new List<CartItem>(); // Giỏ hàng trở về rỗng

            if (vm.PaymentMethod == "CashOnDelivery")
            {
                return RedirectToAction("Success");
            }
            else if (vm.PaymentMethod == "VNPay")
            {
                // TODO: Redirect sang trang VNPay
                return RedirectToAction("VNPayPayment", new { id = bill.Id });
            }

            return RedirectToAction("Checkout");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult VNPayPayment(int id)
        {
            // xử l1y TT vnpay sau
            return Content($"Thanh toán VNPay cho đơn {id}");
        }

    }
}
