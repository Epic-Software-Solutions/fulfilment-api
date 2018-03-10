using EpicSoftware.Fulfilment.Domain;
using EpicSoftware.Fulfilment.Domain.Order;
using Microsoft.EntityFrameworkCore;

namespace EpicSoftware.Fulfilment.Context
{
    public class FulfilmentContext : DbContext
    {
        public FulfilmentContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Order> Order { get; set; }
    }
}