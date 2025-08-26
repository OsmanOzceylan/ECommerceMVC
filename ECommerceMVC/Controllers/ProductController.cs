using ECommerceMVC.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
namespace ECommerceMVC.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        // Ürün listeleme 
        public async Task<IActionResult> Index(int? categoryId, string categoryName, int page = 1, int pageSize = 8)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            var model = await _productService.GetProductListViewModelAsync(categoryId, categoryName, page, pageSize, customerId);
            return View(model);
        }

        // Ürün detay 
        public async Task<IActionResult> Details(int productId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            var favoriteCookie = Request.Cookies["FavoriteProducts"];
            var model = await _productService.GetProductDetailsViewModelAsync(productId, customerId, favoriteCookie);

            if (model == null) return NotFound();
            return View(model);
        }
        // Sepete ekleme 
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal unitPrice, string imageUrl)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
                return RedirectToAction("CustomerLogin", "Account");

            var result = _cartService.AddToCart(productId, productName, unitPrice, imageUrl);
            TempData[result.Success ? "SuccessMessage" : "ErrorMessage"] = result.Message;

            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer)
                ? Redirect(referer) //koşul sağlanırsa geldiği sayfaya yönlendirir
                : RedirectToAction("Index", "Product"); //nullsa anasayfaya yönlendirir
        }
    }
}
 