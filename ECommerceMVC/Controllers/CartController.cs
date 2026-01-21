using ECommerceMVC.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Kullanıcı giriş kontrolü
        private IActionResult CheckLogin()
        {
            if (HttpContext.Session.GetInt32("CustomerID") == null)
            {
                TempData["ErrorMessage"] = "Sepete erişmek için giriş yapmalısınız.";
                return RedirectToAction("CustomerLogin", "Account");
            }
            return null;
        }

        public IActionResult Index()
        {
            var loginCheck = CheckLogin();
            if (loginCheck != null) return loginCheck;

            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal unitPrice, string imageUrl)
        {
            var loginCheck = CheckLogin();
            if (loginCheck != null) return loginCheck;

            var result = _cartService.AddToCart(productId, productName, unitPrice, imageUrl);

            if (result.Success)
                TempData["Message"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(int productId)
        {
            var loginCheck = CheckLogin();
            if (loginCheck != null) return loginCheck;

            _cartService.IncreaseQuantity(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int productId)
        {
            var loginCheck = CheckLogin();
            if (loginCheck != null) return loginCheck;

            _cartService.DecreaseQuantity(productId);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ClearCart()
        {
            var loginCheck = CheckLogin();
            if (loginCheck != null) return loginCheck;

            _cartService.ClearCart();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var loginCheck = CheckLogin();
            if (loginCheck != null) return loginCheck;

            var cartItems = _cartService.GetCartItems();
            cartItems.RemoveAll(x => x.ProductId == productId);
            _cartService.SaveCartItems(cartItems);
            return RedirectToAction("Index");
        }
    }
}
