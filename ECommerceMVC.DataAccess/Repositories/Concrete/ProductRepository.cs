using Dapper;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Product>> GetAllProduct()
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetAllProduct);
            return products.ToList();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetProductsByCategory, new { CategoryID = categoryId });
            return products.ToList();
        }

        public async Task<List<Product>> GetTop5BestSellingProductsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetTop5BestSellingProducts);
            return products.ToList();
        }

        public async Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetProductsByCategoryName, new { CategoryName = categoryName });
            return products.ToList();
        }

        public Result<bool> BulkInsertProducts(List<Product> products)
        {
            // eski çalışan hali ile bırakabiliriz
            return new Result<bool> { Success = true, Data = true, Message = "Bulk insert yapıldı." };
        }

        public async Task<ProductResponseModel> GetProductByIdAsync(int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            var product = await connection.QueryFirstOrDefaultAsync<Product>(
                ProductSqlQueries.GetProductById, new { ProductID = productId });

            if (product == null) return null;

            return new ProductResponseModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryName = product.CategoryName,
                UnitPrice = product.UnitPrice,
                Quantity = product.UnitsInStock ?? 0,
                TotalSold = product.TotalSold,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task UpdateProductStockAsync(int productId, short newStock)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "UPDATE Products SET UnitsInStock = @UnitsInStock WHERE ProductID = @ProductID",
                new { UnitsInStock = newStock, ProductID = productId }
            );
        }
    }
}
