using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBookStore.Helpers;
using WebBookStore.Models;
using WebBookStore.Repositories;
using WebBookStore.Services;
using WebBookStore.ViewModels;

namespace WebBookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private const string CartKey = "MYCART";
        private readonly BookDbContext _context;
        private readonly IMomoService _momoService;  

        public CartController(BookDbContext context, IMomoService momoService)  
        {
            _context = context;
            _momoService = momoService;
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
            if (quantity == 0)
            {
                TempData["Error"] = "Số lượng phải lớn hơn 0.";
                return RedirectToAction("Details", "Book", new { id = id });
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            var cartItems = Cart;
            var existingItem = cartItems.FirstOrDefault(item => item.Book.Id == id);

            int totalQuantity = quantity;
            if (existingItem != null)
            {
                totalQuantity += existingItem.Quantity;
            }

            if (totalQuantity > book.Quantity)
            {
                TempData["Error"] = "Số lượng lón hơn số lượng hàng có sẵn.";
                return RedirectToAction("Details", "Book", new { id = id });
            }

            if (existingItem == null)
            {
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
            TempData["Success"] = "Đã thêm vào giỏ hàng!";
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

            var bill = new Bill
            {
                FullName = vm.FullName,
                Address = vm.Address,
                PhoneNumber = vm.PhoneNumber,
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
                    product.Quantity -= cartItem.Quantity;
                    product.soldQuantity += cartItem.Quantity;
                    _context.Books.Update(product);
                }
            }

            await _context.SaveChangesAsync();

            Cart = new List<CartItem>(); 

            if (vm.PaymentMethod == "CashOnDelivery")
            {
                return RedirectToAction("Success");
            }
            else if (vm.PaymentMethod == "Momo")
            {
                var orderInfo = new OrderInfo
                {
                    FullName = vm.FullName,
                    Amount = (long)vm.TotalAmount,
                    OrderInfomation = $"Thanh toán đơn hàng #{bill.Id}"
                };

                var response = await _momoService.CreatePaymentAsync(orderInfo);
                if (response != null && !string.IsNullOrEmpty(response.PayUrl))
                {
                    return Redirect(response.PayUrl);
                }

                // fallback nếu lỗi
                return RedirectToAction("Checkout");
            }

            return RedirectToAction("Checkout");
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
