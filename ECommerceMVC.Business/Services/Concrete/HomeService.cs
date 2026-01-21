using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Core.Models.ViewModels;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IFavoriteService _favoriteService;

        public HomeService(
            IProductService productService,
            ICategoryService categoryService,
            IFavoriteService favoriteService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _favoriteService = favoriteService;
        }

        public async Task<HomeIndexViewModel> GetHomePageDataAsync(int? customerId, int page, int pageSize)
        {
            // Tüm kategorileri al
            var categoriesFromDb = await _categoryService.GetAllCategoriesAsync();

            // Category -> CategoryResponseModel dönüşümü
            var categories = categoriesFromDb
                .Select(c => new CategoryResponseModel
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName
                })
                .ToList();

            // Sayfalı ürünleri al
            var pagedResult = await _productService.GetPagedProductsAsync(page, pageSize);

            // Favori ürünler
            var favoriteIds = customerId != null
                ? _favoriteService.GetFavoriteIdsByCustomer(customerId.Value)
                : new List<int>();

            // ViewModel oluştur
            return new HomeIndexViewModel
            {
                Categories = categories,
                TopSellingProducts = await _productService.GetTop5BestSellingProductsAsync(),
                AllProducts = pagedResult.Items,
                CurrentPage = pagedResult.CurrentPage,
                PageSize = pagedResult.PageSize,
                TotalProducts = pagedResult.TotalCount,
                FavoriteIds = favoriteIds
            };
        }
    }
}
