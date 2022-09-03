using Odevez.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IOrderRepository
    {
        Task CreatAsync(OrderModel order);
        Task CreatItemAsync(OrderItemModel item);
        Task UpdateAsync(OrderModel order);
        Task UpdateItemAsync(OrderModel order);
        Task DeleteAsync(int orderId);
        Task DeleteItemAsync(int orderId);
        Task<OrderModel> GetByIdAsync(int orderId);
        Task<bool> ExistsByIdAsync(int orderId);
        Task<List<OrderModel>> ListByFilterAsync(int? orderId = 0, int? clientId = 0, int? userId = 0);
        Task<List<OrderItemModel>> ListItemByOrderIdAsync(int? orderId = 0);
    }
}
