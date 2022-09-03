using AutoMapper;
using Odevez.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Models;
using Odevez.Repository.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderBusiness(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task CreatAsync(OrderDTO order)
        {
            await _orderRepository.CreatAsync(_mapper.Map<OrderModel>(order));
        }

        public async Task CreatItemAsync(OrderItemDTO item)
        {
            await _orderRepository.CreatItemAsync(_mapper.Map<OrderItemModel>(item));
        }

        public async Task DeleteAsync(int orderId)
        {
            await _orderRepository.DeleteAsync(orderId);
        }

        public async Task DeleteItemAsync(int orderId)
        {
            await _orderRepository.DeleteItemAsync(orderId);
        }

        public async Task<bool> ExistsByIdAsync(int orderId)
        {
            return await _orderRepository.ExistsByIdAsync(orderId);
        }

        public async Task<OrderDTO> GetByIdAsync(int orderId)
        {
            var retorno = await _orderRepository.GetByIdAsync(orderId);
            return _mapper.Map<OrderDTO>(retorno);
        }

        public async Task<List<OrderDTO>> ListByFilterAsync(int? orderId = 0, int? clientId = 0, int? userId = 0)
        {
            var retorno = await _orderRepository.ListByFilterAsync(orderId, clientId, userId);
            return _mapper.Map<List<OrderDTO>>(retorno);
        }

        public async Task<List<OrderItemDTO>> ListItemByOrderIdAsync(int? orderId = 0)
        {
            var retorno = await _orderRepository.ListItemByOrderIdAsync(orderId);
            return _mapper.Map<List<OrderItemDTO>>(retorno);
        }

        public async Task UpdateAsync(OrderDTO order)
        {
            await _orderRepository.UpdateAsync(_mapper.Map<OrderModel>(order));
        }

        public async Task UpdateItemAsync(OrderDTO order)
        {
            await _orderRepository.UpdateAsync(_mapper.Map<OrderModel>(order));
        }
    }
}
