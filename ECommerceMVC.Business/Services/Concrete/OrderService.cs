using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using FluentValidation;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;
        private readonly ICustomerService _customerService;
        private readonly IProductRepository _productRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICartService cartService,
            ICustomerService customerService,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
            _customerService = customerService;
            _productRepository = productRepository;
        }

        // Interface uyumlu
        public async Task<CheckoutRequest> GetCheckoutRequestAsync(int? customerId)
        {
            var model = new CheckoutRequest();

            if (customerId.HasValue)
            {
                var profileResult = _customerService.GetCustomerById(customerId.Value);
                if (profileResult.Success)
                {
                    model.FirstName = profileResult.Data.CustomerName;
                    model.LastName = profileResult.Data.CustomerLastName;
                    model.Email = profileResult.Data.Email;
                    model.Address = profileResult.Data.Address;
                    model.City = profileResult.Data.City;
                    model.Country = profileResult.Data.Country;
                    model.PostalCode = profileResult.Data.PostalCode;
                    model.PhoneNumber = profileResult.Data.Phone;
                }
            }

            return model;
        }

        public async Task<(bool Success, string Message)> ProcessCheckoutAsync(int? customerId, CheckoutRequest model)
        {
            var validator = new CheckoutRequestValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return (false, errors);
            }

            if (!customerId.HasValue)
                return (false, "Lütfen önce giriş yapın.");

            var cartItems = _cartService.GetCartItems();
            if (cartItems.Count == 0)
                return (false, "Sepetiniz boş, önce ürün ekleyin.");

            var order = new Order
            {
                CustomerID = customerId.Value,
                OrderDate = DateTime.Now
            };

            var orderId = await _orderRepository.CreateOrderAsync(order);

            foreach (var item in cartItems)
            {
                // Ürünü al
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null)
                    return (false, $"Ürün bulunamadı: {item.ProductId}");

                // Burada Quantity artık UnitsInStock değerini tutuyor
                if (product.Quantity < item.Quantity)
                    return (false, $"{product.ProductName} için yeterli stok yok!");

                // Sipariş detayını ekle
                var orderDetail = new OrderDetail
                {
                    OrderID = orderId,
                    ProductID = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };
                await _orderRepository.CreateOrderDetailAsync(orderDetail);

                // Stoktan düş
                var newStock = (short)(product.Quantity - item.Quantity);
                await _productRepository.UpdateProductStockAsync(product.ProductID, newStock);
            }

            var orderInfo = new OrderInfo
            {
                OrderID = orderId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                City = model.City,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber,
            };
            await _orderRepository.CreateOrderInfoAsync(orderInfo);

            _cartService.ClearCart();

            return (true, $"Siparişiniz başarıyla oluşturuldu. Sipariş ID: {orderId}");
        }
    }
}
