using ECommerceMVC.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 8)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID"); // sadece session alıyoruz
            var viewModel = await _homeService.GetHomePageDataAsync(customerId, page, pageSize);
            return View(viewModel);
        }
    }
}
