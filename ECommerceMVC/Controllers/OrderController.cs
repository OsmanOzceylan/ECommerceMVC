using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            var model = await _orderService.GetCheckoutRequestAsync(customerId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutRequest model)
        {
            // Hataları temizleyerek başlıyoruz, böylece eski veya yanlış hatalar görüntülenmez.
            ModelState.Clear();

            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            var result = await _orderService.ProcessCheckoutAsync(customerId, model);

            if (!result.Success)
            {
                // Fluent Validation hatalarını tek tek ayır ve ekle.
                var errorMessages = result.Message.Split(", ").ToList();
                foreach (var error in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                // Kullanıcı verilerini kaybetmeden aynı View'ı döndür.
                return View(model);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index", "Product");
        }
    }
}