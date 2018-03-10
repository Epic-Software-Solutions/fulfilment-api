using System;
using System.Threading.Tasks;
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
    }
}