using Dapper;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Result<Customer> GetCustomerByCustomerName(string customerName)
        {
            using var connection = new SqlConnection(_connectionString);
            var customer = connection.QueryFirstOrDefault<Customer>(
                CustomerQueries.GetCustomerByCustomerName,
                new { CustomerName = customerName }
            );

            if (customer != null)
                return Result<Customer>.Ok(customer, "Müşteri bulundu.");
            return Result<Customer>.Fail("Müşteri bulunamadı.");
        }

        public Result<Customer> GetCustomerInformation(string customerName, string customerPassword)
        {
            using var connection = new SqlConnection(_connectionString);
            var customer = connection.QueryFirstOrDefault<Customer>(
                CustomerQueries.GetCustomerInfo,
                new { CustomerName = customerName, CustomerPassword = customerPassword }
            );

            if (customer != null)
                return Result<Customer>.Ok(customer, "Müşteri bilgileri doğru.");
            return Result<Customer>.Fail("Kullanıcı adı veya şifre hatalı.");
        }

        public Result<string> CreateCustomer(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            int rowsAffected = connection.Execute(
                CustomerQueries.CreateCustomer,
                new
                {
                    CustomerName = customer.CustomerName,
                    CustomerPassword = customer.CustomerPassword,
                    CustomerLastName = customer.CustomerLastName,
                    Email = customer.Email,
                    Address = customer.Address,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone
                }
            );

            if (rowsAffected > 0)
                return Result<string>.Ok("Müşteri başarıyla oluşturuldu.");
            return Result<string>.Fail("Müşteri oluşturulamadı.");
        }

        public Result<Customer> GetCustomerById(int customerId)
        {
            using var connection = new SqlConnection(_connectionString);
            var customer = connection.QueryFirstOrDefault<Customer>(
                CustomerQueries.GetCustomerByCustomerID,
                new { CustomerID = customerId }
            );

            if (customer != null)
                return Result<Customer>.Ok(customer, "Müşteri bulundu.");
            return Result<Customer>.Fail("Müşteri bulunamadı.");
        }

        public Result<string> UpdateCustomer(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            int rowsAffected = connection.Execute(
                CustomerQueries.UpdateCustomer,
                new
                {
                    CustomerID = customer.CustomerID,
                    CustomerName = customer.CustomerName,
                    CustomerLastName = customer.CustomerLastName,
                    Email = customer.Email,
                    Address = customer.Address,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Phone = customer.Phone
                }
            );

            if (rowsAffected > 0)
                return Result<string>.Ok("Müşteri bilgileri güncellendi.");
            return Result<string>.Fail("Müşteri bilgileri güncellenemedi.");
        }
    }
}
