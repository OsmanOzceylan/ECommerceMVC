using Dapper;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly string _connectionString;

        public FavoriteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddToFavorites(int customerId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(FavoriteQueries.AddToFavorites, new { CustomerID = customerId, ProductID = productId });
        }

        public bool FavoriteExists(int customerId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.ExecuteScalar<int>(FavoriteQueries.FavoriteExists, new { CustomerID = customerId, ProductID = productId }) > 0;
        }

        public List<Favorite> GetFavoritesByCustomer(int customerId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Favorite>(FavoriteQueries.GetFavoritesByCustomerID, new { CustomerID = customerId }).ToList();
        }

        public void RemoveFromFavorites(int customerId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(FavoriteQueries.RemoveFromFavorites, new { CustomerID = customerId, ProductID = productId });
        }

        public void ClearFavorites(int customerId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(FavoriteQueries.ClearFavorites, new { CustomerID = customerId });
        }
    }
}
