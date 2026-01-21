using ECommerceMVC.Business.Helper;
using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Kullanıcı girişi doğrulama
        public Result<Customer> GetCustomerInformation(string customerName, string customerPassword)
        {
            var hashedPassword = PasswordHelper.HashPassword(customerPassword);
            var result = _customerRepository.GetCustomerInformation(customerName, hashedPassword);

            if (!result.Success || result.Data == null)
                return Result<Customer>.Fail("Geçersiz kullanıcı adı veya şifre.");

            return Result<Customer>.Ok(result.Data, result.Message);
        }
        public Result<Customer> GetCustomerById(int customerId)  // yeni metot
        {
            var result = _customerRepository.GetCustomerById(customerId);

            if (!result.Success || result.Data == null)
                return Result<Customer>.Fail("Müşteri bulunamadı.");

            return Result<Customer>.Ok(result.Data, result.Message);
        }

        // Müşteri oluşturma
        public Result<string> CreateCustomer(Customer customer)
        {
            customer.CustomerPassword = PasswordHelper.HashPassword(customer.CustomerPassword);
            var result = _customerRepository.CreateCustomer(customer);

            if (!result.Success)
                return Result<string>.Fail(result.Message);

            return Result<string>.Ok("Müşteri başarıyla oluşturuldu.");
        }

        // CustomerName ile müşteri getir
        public Result<Customer> GetCustomerByCustomerName(string customerName)
        {
            var result = _customerRepository.GetCustomerByCustomerName(customerName);

            if (!result.Success || result.Data == null)
                return Result<Customer>.Fail("Müşteri bulunamadı.");

            return Result<Customer>.Ok(result.Data, result.Message);
        }

        // Register işlemi
        public Result<string> RegisterCustomer(CustomerRegisterRequest model)
        {
            var existingUser = _customerRepository.GetCustomerByCustomerName(model.CustomerName);
            if (existingUser.Success && existingUser.Data != null)
                return Result<string>.Fail("Bu kullanıcı adı zaten mevcut.");

            var newCustomer = new Customer
            {
                CustomerName = model.CustomerName,
                CustomerPassword = PasswordHelper.HashPassword(model.CustomerPassword)
            };

            var result = _customerRepository.CreateCustomer(newCustomer);
            if (!result.Success)
                return Result<string>.Fail(result.Message);

            return Result<string>.Ok("Kayıt işlemi başarıyla tamamlandı.");
        }

        public Result<string> UpdateCustomer(Customer customer)
        {
            var existingCustomer = _customerRepository.GetCustomerByCustomerName(customer.CustomerName);

            if (!existingCustomer.Success || existingCustomer.Data == null)
                return Result<string>.Fail("Güncellenecek müşteri bulunamadı.");

            // Şifre değişmemişse mevcut şifreyi koru, değişmişse hashle
            if (!string.IsNullOrEmpty(customer.CustomerPassword))
                customer.CustomerPassword = PasswordHelper.HashPassword(customer.CustomerPassword);
            else
                customer.CustomerPassword = existingCustomer.Data.CustomerPassword;

            _customerRepository.UpdateCustomer(customer); // repository’de bu metod olmalı
            return Result<string>.Ok("Müşteri bilgileri başarıyla güncellendi.");
        }
    }
}
