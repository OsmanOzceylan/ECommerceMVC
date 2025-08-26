using ECommerceMVC.Business.Services;
using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly EmailService _emailService;

        public OrderController(IOrderService orderService, EmailService emailService)
        {
            _orderService = orderService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            var model = await _orderService.GetCheckoutRequestAsync(customerId);
            return View(model);
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutRequest model)
        {
            ModelState.Clear();
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            var result = await _orderService.ProcessCheckoutAsync(customerId, model);

            if (!result.Success)
            {
                var errorMessages = result.Message.Split(", ").ToList();
                foreach (var error in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(model);
            }

            // Mail gönderimi (Hangfire ile)
            if (!string.IsNullOrEmpty(result.CustomerEmail))
            {
                BackgroundJob.Enqueue(() =>
                    _emailService.SendEmail(result.CustomerEmail,
                                            "Sipariş Onayı",
                                            $"Siparişiniz {result.OrderId} numarasıyla alınmıştır."));
            }

            // Başarı durumunda yönlendirme (mail olmasa bile çalışır)
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index", "Product");
        }

    }
}