using System;
using System.Linq;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EpicSoftware.Fulfilment.Api.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrdersService.OrdersService _service;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(OrdersService.OrdersService service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllOpenOrders()
        {
            try
            {
                _logger.LogTrace(0, "Get all open orders");
                return Ok(await _service.GetAllOpenOrders());
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder([FromBody] OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(3, "Order modelstate invalid");
                var messages = string.Join(";",
                    ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(new {Error = messages});
            }
       
            try
            {
                _logger.LogTrace(0, "Create new order");
                await _service.CreateNewOrder(order);
                return Created("", new {Message = "Order has been created successfully"});
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
                throw;
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                _logger.LogTrace(0, $"Get order by ID {orderId}");
                return Ok(await _service.GetOrderById(orderId));
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
                throw;
            }
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderById(int orderId)
        {
            try
            {
                _logger.LogTrace(0, $"Delete order by ID {orderId}");
                await _service.DeleteOrder(orderId);
                return Ok(new { Message = $"Order {orderId} has been deleted successfully" });
            }
            catch (Exception e)
            {
                _logger.LogError(5, e.InnerException.Message);
                throw;
            }
        }
    }
}