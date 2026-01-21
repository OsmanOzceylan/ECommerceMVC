using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface IFavoriteRepository
    {
        void AddToFavorites(int customerId, int productId);
        bool FavoriteExists(int customerId, int productId);
        List<Favorite> GetFavoritesByCustomer(int customerId);
        void RemoveFromFavorites(int customerId, int productId);
        void ClearFavorites(int customerId);
    }
}
