using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Context;
using EpicSoftware.Fulfilment.Domain.Order;
using Microsoft.EntityFrameworkCore;

namespace EpicSoftware.Fulfilment.Repository.Orders
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(FulfilmentContext context) : base(context)
        {
        }

        /// <summary>
        /// Implementation that queries the database for all open orders
        /// </summary>
        /// <returns>List of Orders</returns>
        public Task<List<Order>> GetAllOpenOrders()
        {
            return DbSet.AsNoTracking().Where(x => !x.OrderComplete).ToListAsync();
        }

        /// <summary>
        /// Implementation that queries the database for a single order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Order</returns>
        public Task<Order> GetOrderById(int orderId)
        {
            return DbSet.AsNoTracking().Where(x => x.Id == orderId).SingleOrDefaultAsync();
        }
    }
}