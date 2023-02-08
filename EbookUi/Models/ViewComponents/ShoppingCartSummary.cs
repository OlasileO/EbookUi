using EbookUi.Models.Shoppingcart;
using Microsoft.AspNetCore.Mvc;

namespace EbookUi.Models.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartSummary(ShoppingCart _shoppingCart)
        {
            shoppingCart = _shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var item = shoppingCart.GetshoppingCartItems();

            return View(item.Count);
        }
    }
}
