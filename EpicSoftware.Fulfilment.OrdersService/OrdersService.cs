using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EpicSoftware.Fulfilment.Domain.Order;
using EpicSoftware.Fulfilment.Dtos.Orders;
using EpicSoftware.Fulfilment.Repository.Orders;

namespace EpicSoftware.Fulfilment.OrdersService
{
    public class OrdersService
    {
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;
        
        public OrdersService(IOrderRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all open orders from repository
        /// </summary>
        /// <returns>List of open orders</returns>
        public async Task<List<Order>> GetAllOpenOrders()
        {
            try
            {
                return await _ordersRepository.GetAllOpenOrders();
            }
            catch (Exception e)
            {
                //TODO Add Logging
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Create a new order in the repository
        /// </summary>
        /// <param name="order"></param>
        public async Task CreateNewOrder(OrderDto order)
        {
            try
            {
                await _ordersRepository.Create(_mapper.Map<Order>(order));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}