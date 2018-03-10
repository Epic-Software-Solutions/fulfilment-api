using System;
using System.Linq;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;

namespace EpicSoftware.Fulfilment.Api.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrdersService.OrdersService _service;

        public OrdersController(OrdersService.OrdersService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllOpenOrders()
        {
            try
            {
                return Ok(await _service.GetAllOpenOrders());
            }
            catch (Exception e)
            {
                //TODO Add logging
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder([FromBody] OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                var messages = string.Join(";",
                    ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(new {Error = messages});
            }
       
            try
            {
                await _service.CreateNewOrder(order);
                return Created("", new {Message = "Order has been created successfully"});
            }
            catch (Exception e)
            {
                //TODO Add logging
                Console.WriteLine(e);
                throw;
            }
        }
    }
}