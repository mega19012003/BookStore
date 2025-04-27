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

        // POST: /Customer/Cart/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                //decimal shippingFee = 30000;
                //decimal totalCart = Cart.Sum(x => x.TotalPrice);

                var bill = new Bill
                {
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Note = model.Note,
                    OrderDate = DateTime.Now,
                    //TotalAmount = totalCart + shippingFee, // Tổng tiền + phí ship
                    TotalAmount = Cart.Sum(x => x.TotalPrice),
                    //ShippingFee = shippingFee,
                    PaymentMethod = model.PaymentMethod
                };

                _context.Bills.Add(bill);
                _context.SaveChanges();

                if (model.PaymentMethod == "VNPAY")
                {
                    // Nếu chọn VNPAY thì chuyển đến trang thanh toán
                    return RedirectToAction("VnpayPayment", "Payment", new { billId = bill.Id }); // <-- sửa đúng là BillId
                }
                else if (model.PaymentMethod == "COD")
                {
                    // Nếu chọn COD thì trả về trang thành công luôn
                    return RedirectToAction("OrderSuccess");
                }
            }

            return View(Cart);
        }
    }
}
