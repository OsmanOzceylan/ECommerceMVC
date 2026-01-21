using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProduct();
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<List<Product>> GetTop5BestSellingProductsAsync();
        Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName);
        Result<bool> BulkInsertProducts(List<Product> products);
        Task<ProductResponseModel> GetProductByIdAsync(int productId);
        Task UpdateProductStockAsync(int productId, short newStock);
    }
}
