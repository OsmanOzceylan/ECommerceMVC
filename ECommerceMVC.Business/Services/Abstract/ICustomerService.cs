using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface ICustomerService
    {
        Result<Customer> GetCustomerInformation(string customerName, string customerPassword);
        Result<Customer> GetCustomerByCustomerName(string customerName);
        Result<string> CreateCustomer(Customer customer);
        Result<Customer> GetCustomerById(int customerId);
        Result<string> RegisterCustomer(CustomerRegisterRequest model);
        Result<string> UpdateCustomer(Customer customer);
    }
}
