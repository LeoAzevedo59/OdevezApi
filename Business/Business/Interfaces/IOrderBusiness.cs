using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Interfaces
{
    public interface IOrderBusiness
    {
        Task CreatAsync(OrderDTO order);
        Task CreatItemAsync(OrderItemDTO item);
        Task UpdateAsync(OrderDTO order);
        Task UpdateItemAsync(OrderDTO order);
        Task DeleteAsync(int orderId);
        Task DeleteItemAsync(int orderId);
        Task<OrderDTO> GetByIdAsync(int orderId);
        Task<bool> ExistsByIdAsync(int orderId);
        Task<List<OrderDTO>> ListByFilterAsync(int? orderId = 0, int? clientId = 0, int? userId = 0);
        Task<List<OrderItemDTO>> ListItemByOrderIdAsync(int? orderId = 0);
    }
}
