using Dapper;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            using var connection = new SqlConnection(_connectionString);
            var orderId = await connection.QuerySingleAsync<int>(OrderQueries.CreateOrder, new { order.CustomerID, order.OrderDate });
            return orderId;
        }

        public async Task CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(OrderQueries.CreateOrderDetail, new { orderDetail.OrderID, orderDetail.ProductID, orderDetail.Quantity, orderDetail.UnitPrice, orderDetail.IsActive });
        }

        public async Task CreateOrderInfoAsync(OrderInfo orderInfo)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(OrderQueries.CreateOrderInfo, new
            {
                orderInfo.OrderID,
                orderInfo.FirstName,
                orderInfo.LastName,
                orderInfo.Email,
                orderInfo.Address, 
                orderInfo.City,
                orderInfo.PostalCode,
                orderInfo.PhoneNumber,
                orderInfo.CardNumber,
                orderInfo.CardHolderName,
                orderInfo.CVV
            });
        }
    }
}
