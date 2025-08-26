using ECommence.Core.Models.Response;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IOrderService
    {
        // Checkout sayfası için model hazırla (0 iş controller)
        Task<CheckoutRequest> GetCheckoutRequestAsync(int? customerId);

        // Sepet ve checkout ile siparişi işle
        Task<CheckoutResult> ProcessCheckoutAsync(int? customerId, CheckoutRequest model);
    }
}
