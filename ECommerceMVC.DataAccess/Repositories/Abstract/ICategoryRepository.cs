using ECommerceMVC.Entities.Models;
namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
