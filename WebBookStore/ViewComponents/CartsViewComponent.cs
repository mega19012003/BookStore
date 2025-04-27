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
            var cart = HttpContext.Session.GetObjectFromJson<List<ViewModels.CartItem>>(CartKey.CART_KEY) ?? new List<ViewModels.CartItem>();

            return View("CartPanel", new CartModel
            {
                quantity = cart.Sum(p => p.Quantity),
                total = cart.Sum(p => p.TotalPrice)
            });
        }
    }
}
