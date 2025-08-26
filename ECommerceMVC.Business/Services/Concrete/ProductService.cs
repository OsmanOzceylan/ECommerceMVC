using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Core.Models.ViewModels;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFavoriteService _favoriteService;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(
            IProductRepository productRepository,
            IFavoriteService favoriteService,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _favoriteService = favoriteService;
            _categoryRepository = categoryRepository;
        }

        // --- Temel ürün sorguları ---
        public async Task<List<ProductResponseModel>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProduct();
            return products.Select(MapToResponse).ToList();
        }

        public async Task<List<ProductResponseModel>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            return products.Select(MapToResponse).ToList();
        }

        public async Task<List<ProductResponseModel>> GetProductsByCategoryNameAsync(string categoryName)
        {
            var products = await _productRepository.GetProductsByCategoryNameAsync(categoryName);
            return products.Select(MapToResponse).ToList();
        }

        public async Task<List<ProductResponseModel>> GetTop5BestSellingProductsAsync()
        {
            var products = await _productRepository.GetTop5BestSellingProductsAsync();
            return products.Select(MapToResponse).ToList();
        }

        public bool BulkInsertProducts(List<Product> products)
        {
            var result = _productRepository.BulkInsertProducts(products);
            if (result.Success) return result.Data;

            Console.WriteLine(result.Message);
            return false;
        }

        public async Task<ProductResponseModel?> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        // --- Controller için ViewModel metodları ---
        public async Task<ProductListViewModel> GetProductListViewModelAsync(int? categoryId, string categoryName, int page, int pageSize, int? customerId)
        {
            List<ProductResponseModel> products;

            if (categoryId.HasValue)
                products = await GetProductsByCategoryAsync(categoryId.Value);
            else if (!string.IsNullOrEmpty(categoryName))
                products = await GetProductsByCategoryNameAsync(categoryName);
            else
                products = await GetAllProductsAsync();

            var pagedProducts = products
                .Skip((page - 1) * pageSize) //atlatılan ürün sayısı
                .Take(pageSize) //alınan ürün sayısı
                .ToList(); // seçilen ürün listesi

            var favoriteIds = customerId.HasValue
                ? _favoriteService.GetFavoriteIdsByCustomer(customerId.Value)
                : new List<int>();

            var categoriesFromDb = await _categoryRepository.GetAllCategoriesAsync();
            var categories = categoriesFromDb.Select(c => new CategoryResponseModel
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName
            }).ToList();

            return new ProductListViewModel
            {
                Products = pagedProducts,
                CurrentPage = page,
                PageSize = pageSize,
                TotalProducts = products.Count,
                FavoriteIds = favoriteIds,
                Categories = categories
            };
        }

        public async Task<ProductResponseModel?> GetProductDetailsViewModelAsync(
            int productId,
            int? customerId,
            string favoriteCookie)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null) return null;

            product.IsFavorite = _favoriteService.IsFavorite(productId, customerId, favoriteCookie);
            return product;
        }

    // --- PagedResult eski uyumluluk ---
        public async Task<PagedResult<ProductResponseModel>> GetPagedProductsAsync(int page, int pageSize, int? categoryId = null, string categoryName = null)
        {
            List<ProductResponseModel> products;

            if (categoryId.HasValue)
                products = await GetProductsByCategoryAsync(categoryId.Value);
            else if (!string.IsNullOrEmpty(categoryName))
                products = await GetProductsByCategoryNameAsync(categoryName);
            else
                products = await GetAllProductsAsync();

            return new PagedResult<ProductResponseModel>
            {
                Items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = products.Count
            };
        }
        private ProductResponseModel MapToResponse(dynamic p)
        {
            return new ProductResponseModel
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                CategoryName = p.CategoryName,
                UnitPrice = p.UnitPrice,
                ImageUrl = p.ImageUrl,
                IsFavorite = false // default değer ataması 
            };
        }
    }
}
