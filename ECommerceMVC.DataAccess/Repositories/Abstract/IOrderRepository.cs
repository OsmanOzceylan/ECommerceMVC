using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order);
        Task CreateOrderDetailAsync(OrderDetail orderDetail);
        Task CreateOrderInfoAsync(OrderInfo orderInfo);
    }
}
