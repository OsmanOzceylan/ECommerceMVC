using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;

        public AccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // --- Customer Login ---
        [HttpGet]
        public IActionResult CustomerLogin() => View();

        [HttpPost]
        public IActionResult CustomerLogin(CustomerLoginRequestModel model)
        {
            if (!ModelState.IsValid) //veri doğruluğunu kontrol eder
                return View(model);

            var result = _customerService.GetCustomerInformation(model.CustomerName, model.CustomerPassword);

            if (result.Success)
            {
                HttpContext.Session.SetString("CustomerName", result.Data.CustomerName);
                HttpContext.Session.SetInt32("CustomerID", result.Data.CustomerID);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = result.Message;
            return View(model);
        }

        // --- Customer Register ---
        [HttpGet]
        public IActionResult CustomerRegister() => View();

        [HttpPost]
        public IActionResult CustomerRegister(CustomerRegisterRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _customerService.RegisterCustomer(model);

            if (result.Success)
                return RedirectToAction("CustomerLogin");

            ViewBag.Error = result.Message;
            return View(model);
        }

        // --- Customer Logout ---
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
