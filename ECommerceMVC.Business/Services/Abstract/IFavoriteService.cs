using ECommerceMVC.Core.Models.Response;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IFavoriteService
    {
        void AddToFavorites(int customerId, int productId);
        bool FavoriteExists(int customerId, int productId);
        List<ProductResponseModel> GetFavoritesByCustomer(int customerId);
        List<int> GetFavoriteIdsByCustomer(int customerId);
        bool IsFavorite(int productId, string cookieValue);
        bool IsFavorite(int productId, int? customerId, string cookieValue);
        void RemoveFromFavorites(int customerId, int productId);
        void ClearFavorites(int customerId);
    }
}
