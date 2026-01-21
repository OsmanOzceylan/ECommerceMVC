using ECommerceMVC.Core.Models.Response;
namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IHomeService
    {
        Task<HomeIndexViewModel> GetHomePageDataAsync(int? customerId, int page, int pageSize);
    }

}
