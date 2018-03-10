using System.Collections.Generic;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Domain.Order;

namespace EpicSoftware.Fulfilment.Repository.Orders
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<List<Order>> GetAllOpenOrders();
    }
}