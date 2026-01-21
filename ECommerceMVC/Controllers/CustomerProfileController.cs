using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class CustomerProfileController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerProfileController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
                return RedirectToAction("CustomerLogin", "Account");

            var result = _customerService.GetCustomerById(customerId.Value);
            return View(result.Data ?? new Customer());
        }

        [HttpPost]
        public IActionResult Profile(Customer model)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
                return RedirectToAction("CustomerLogin", "Account");

            model.CustomerID = customerId.Value;

            var result = _customerService.UpdateCustomer(model);

            ViewBag.Success = result.Success ? result.Message : null;
            ViewBag.Error = !result.Success ? result.Message : null;

            return View(model);
        }
    }
}
