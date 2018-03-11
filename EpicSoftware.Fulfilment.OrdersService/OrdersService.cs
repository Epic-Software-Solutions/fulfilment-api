using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EpicSoftware.Fulfilment.Domain.Order;
using EpicSoftware.Fulfilment.Dtos.Orders;
using EpicSoftware.Fulfilment.Repository.Orders;
using Microsoft.Extensions.Logging;

namespace EpicSoftware.Fulfilment.OrdersService
{
    public class OrdersService
    {
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersService> _logger;
        
        public OrdersService(IOrderRepository ordersRepository, IMapper mapper, ILogger<OrdersService> logger)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all open orders from repository
        /// </summary>
        /// <returns>List of open orders</returns>
        public async Task<List<Order>> GetAllOpenOrders()
        {
            try
            {
                _logger.LogTrace(0, "Get all open orders via service");
                return await _ordersRepository.GetAllOpenOrders();
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
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
                _logger.LogTrace(0, "Create new order via service");
                await _ordersRepository.Create(_mapper.Map<Order>(order));
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
                throw;
            }
        }

        /// <summary>
        /// Delete an order by ID
        /// </summary>
        /// <param name="orderId"></param>
        public async Task DeleteOrder(int orderId)
        {
            try
            {
                _logger.LogTrace(0, $"Delete order {orderId} via service");
                await _ordersRepository.Delete(orderId);
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Gets a single order by ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Order</returns>
        public async Task<Order> GetOrderById(int orderId)
        {
            try
            {
                _logger.LogTrace(0, $"Get order {orderId} via service");
                return await _ordersRepository.GetOrderById(orderId);
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
                throw;
            }
        }
        
    }
}