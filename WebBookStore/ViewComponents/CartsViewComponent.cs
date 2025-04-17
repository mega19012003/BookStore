using Microsoft.AspNetCore.Mvc;
using WebBookStore.Helpers;
using WebBookStore.Models;
using WebBookStore.ViewModels;

namespace WebBookStore.ViewComponents
{
    public class CartsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get < List<CartItem>>(CartKey.CART_KEY) ?? new List<CartItem>();
            
            return View("CartPanel", new CartModel
            {
                quantity = cart.Sum(p => p.SoLuong),
                total = cart.Sum(p => p.ThanhTien)
            });
        }
    }
}
