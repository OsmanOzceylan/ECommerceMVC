using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Core.Models.ViewModels;
using ECommerceMVC.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IProductService
    {
        // Temel ürün sorguları
        Task<List<ProductResponseModel>> GetAllProductsAsync();
        Task<List<ProductResponseModel>> GetProductsByCategoryAsync(int categoryId);
        Task<List<ProductResponseModel>> GetProductsByCategoryNameAsync(string categoryName);
        Task<List<ProductResponseModel>> GetTop5BestSellingProductsAsync();
        bool BulkInsertProducts(List<Product> products);
        Task<ProductResponseModel> GetProductByIdAsync(int productId);

        // Controller’lar iş mantığını yapmasın diye eklenen ViewModel metotları
        Task<ProductListViewModel> GetProductListViewModelAsync(int? categoryId, string categoryName, int page, int pageSize, int? customerId);
        Task<ProductResponseModel> GetProductDetailsViewModelAsync(int productId, int? customerId, string favoriteCookie);

        // Eski method uyumluluk
        Task<PagedResult<ProductResponseModel>> GetPagedProductsAsync(int page, int pageSize, int? categoryId = null, string categoryName = null);
    }
}
