using ECommerceMVC.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // Favoriler sayfası
        public IActionResult Index()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                TempData["ErrorMessage"] = "Favorilere erişmek için giriş yapmalısınız.";
                return RedirectToAction("CustomerLogin", "Account");
            }

            var favorites = _favoriteService.GetFavoritesByCustomer(customerId.Value);
            return View(favorites);
        }

        // Favorilere ekle
        [HttpPost]
        public IActionResult AddToFavorites(int productId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                TempData["ErrorMessage"] = "Favorilere eklemek için giriş yapmalısınız.";
                return RedirectToAction("CustomerLogin", "Account");
            }

            // Ürün zaten favoride mi kontrol et
            var favorites = _favoriteService.GetFavoritesByCustomer(customerId.Value);
            var existing = favorites.FirstOrDefault(f => f.ProductID == productId);

            if (existing != null)
            {
                _favoriteService.RemoveFromFavorites(customerId.Value, productId);
                TempData["SuccessMessage"] = "Ürün favorilerden çıkarıldı.";
            }
            else
            {
                _favoriteService.AddToFavorites(customerId.Value, productId);
                TempData["SuccessMessage"] = "Ürün favorilere eklendi.";
            }

            // Detay sayfasına geri dön
            return Redirect(Request.Headers["Referer"].ToString());
        }


        // Favoriden kaldır
        [HttpPost]
        public IActionResult RemoveFromFavorites(int productId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId != null)
            {
                _favoriteService.RemoveFromFavorites(customerId.Value, productId);
                TempData["SuccessMessage"] = "Ürün favorilerden silindi.";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Tüm favorileri temizle
        [HttpPost]
        public IActionResult ClearFavorites()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId != null)
            {
                _favoriteService.ClearFavorites(customerId.Value);
                TempData["SuccessMessage"] = "Tüm favoriler temizlendi.";
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        public IActionResult ToggleFavorite(int productId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                TempData["ErrorMessage"] = "Favorilere eklemek için giriş yapmalısınız.";
                return RedirectToAction("CustomerLogin", "Account");
            }

           
            var favorites = _favoriteService.GetFavoritesByCustomer(customerId.Value);
            var existing = favorites.FirstOrDefault(f => f.ProductID == productId);

            if (existing != null)
            {
                
                _favoriteService.RemoveFromFavorites(customerId.Value, productId);
                TempData["SuccessMessage"] = "Ürün favorilerden çıkarıldı.";
            }
            else
            {
               
                _favoriteService.AddToFavorites(customerId.Value, productId);
                TempData["SuccessMessage"] = "Ürün favorilere eklendi.";
            }
            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToAction("Index", "Product");
        }

    }

}

