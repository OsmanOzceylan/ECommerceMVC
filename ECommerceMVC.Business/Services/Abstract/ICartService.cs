using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Core.Utilities;
using System.Collections.Generic;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        void SaveCartItems(List<CartItem> cartItems);
        Result<string> AddToCart(int productId, string productName, decimal unitPrice, string? imageUrl);
        void IncreaseQuantity(int productId);
        void DecreaseQuantity(int productId);
        void ClearCart();
    }
}
