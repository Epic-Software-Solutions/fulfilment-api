using EpicSoftware.Fulfilment.Context;
using EpicSoftware.Fulfilment.Domain;
using EpicSoftware.Fulfilment.Domain.Order;

namespace EpicSoftware.Fulfilment.Repository.Orders
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(FulfilmentContext context) : base(context)
        {
        }
    }
}