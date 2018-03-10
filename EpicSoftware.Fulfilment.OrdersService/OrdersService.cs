using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Domain.Order;
using EpicSoftware.Fulfilment.Repository.Orders;

namespace EpicSoftware.Fulfilment.OrdersService
{
    public class OrdersService
    {
        private readonly IOrderRepository _ordersRepository;
        
        public OrdersService(IOrderRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
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
        
    }
}