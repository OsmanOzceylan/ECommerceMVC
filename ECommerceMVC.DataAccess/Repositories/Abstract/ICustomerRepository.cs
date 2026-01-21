using ECommerceMVC.Core.Utilities;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface ICustomerRepository
    {
        Result<Customer> GetCustomerByCustomerName(string customerName);
        Result<Customer> GetCustomerInformation(string customerName, string customerPassword);
        Result<string> CreateCustomer(Customer customer);
        Result<Customer> GetCustomerById(int customerId);
        Result<string> UpdateCustomer(Customer customer); // Ad, soyad, email, postal code vb. güncelleme
    }
}
