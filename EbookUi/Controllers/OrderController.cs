using Ebook_Service.Abstract;
using Ebook_Service.Implementation;
using EbookUi.Models;
using EbookUi.Models.Shoppingcart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EbookUi.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBookSerivce _bookSerivce;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderService orderService, IBookSerivce bookSerivce, ShoppingCart shoppingCart)
        {
            _orderService=orderService;
            _bookSerivce=bookSerivce;
            _shoppingCart=shoppingCart;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var allorders = await _orderService.GetOrderByUserIdAndRoleAsync(userRole);
            return View(allorders);
        }
        public IActionResult Shoppingcart()
        {
            var item = _shoppingCart.GetshoppingCartItems();
            _shoppingCart.shoppingCartItems = item;
            var reponse = new ShoppingCartVm
            {
                shoppingcart = _shoppingCart,
                shoppingCartTotal = _shoppingCart.GetshoppingCartTotal(),

            };
            return View(reponse);
        }

        public async Task<IActionResult> AddItemshoppincart(int id)
        {
            var book = await _bookSerivce.GetBookByIdAsync(id);
            if (book != null)
            {
                _shoppingCart.AddItemtocart(book);
            }
            return RedirectToAction(nameof(Shoppingcart));
        }

        public async Task<IActionResult> RemoveItemFrmShoppingcart(int id)
        {
            var book = await _bookSerivce.GetBookByIdAsync(id);
            if (book != null)
            {
                _shoppingCart.RemoveItemFrmCart(book);
            }
            return RedirectToAction(nameof(Shoppingcart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var item = _shoppingCart.GetshoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _orderService.StoreOrderAysnc(item, userId);
            await _shoppingCart.clearShoppingCartAsync();
            return View("CompleteOrder");
        }
    }
}
