using Odevez.Business.Interfaces;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class OrderController : IOrderBusiness
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        public async Task CreatAsync(OrderDTO order)
        {
            await _orderBusiness.CreatAsync(order);
        }

        public async Task CreatItemAsync(OrderItemDTO item)
        {
            await _orderBusiness.CreatItemAsync(item);
        }

        public async Task DeleteAsync(int orderId)
        {
            await _orderBusiness.DeleteAsync(orderId);
        }

        public async Task DeleteItemAsync(int orderId)
        {
            await _orderBusiness.DeleteItemAsync(orderId);
        }

        public async Task<bool> ExistsByIdAsync(int orderId)
        {
            return await _orderBusiness.ExistsByIdAsync(orderId);
        }

        public async Task<OrderDTO> GetByIdAsync(int orderId)
        {
            return await _orderBusiness.GetByIdAsync(orderId);
        }

        public async Task<List<OrderDTO>> ListByFilterAsync(int? orderId = 0, int? clientId = 0, int? userId = 0)
        {
            return await _orderBusiness.ListByFilterAsync(orderId, clientId, userId);
        }

        public async Task<List<OrderItemDTO>> ListItemByOrderIdAsync(int? orderId = 0)
        {
            return await _orderBusiness.ListItemByOrderIdAsync(orderId);
        }

        public async Task UpdateAsync(OrderDTO order)
        {
            await _orderBusiness.UpdateAsync(order);
        }

        public async Task UpdateItemAsync(OrderDTO order)
        {
            await _orderBusiness.UpdateAsync(order);
        }
    }
}
