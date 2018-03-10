using System;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Context;
using EpicSoftware.Fulfilment.Domain;
using EpicSoftware.Fulfilment.Domain.Order;
using EpicSoftware.Fulfilment.Repository.Orders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EpicSoftware.Fulfilment.Tests.Orders
{
    public class OrderContextTest
    {
        [Fact]
        public async Task AddNewOrder()
        {
            var repo = await GetInMemoryOrderRepository();
            var order = new Order
            {
                ProductId = "51dssdcsdcsdc",
                ProductJson = "{name: 'Test Product', price: 1.99, currency: 'USD'}",
                OrderDate = DateTime.Now,
                OrderComplete = false,
                UserId = 1,
                Quantity = 1,
                WorkflowId = 1
            };

            await repo.Create(order);
            var total = await repo.GetAll();
            Assert.Equal(1, total.Count);
            Assert.Equal("51dssdcsdcsdc", order.ProductId);
            Assert.Equal(false, order.OrderComplete);
            Assert.Equal(1, order.UserId);
            Assert.Equal(1, order.Quantity);
            Assert.Equal(1, order.WorkflowId);
            Assert.Null(order.OrderCompleteDate);
        }


        private static async Task<IOrderRepository> GetInMemoryOrderRepository()
        {
            var builder = new DbContextOptionsBuilder<FulfilmentContext>();
            builder.UseInMemoryDatabase("Test");
            var options = builder.Options;
            var context = new FulfilmentContext(options);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            return new OrderRepository(context);
        }
    }
}