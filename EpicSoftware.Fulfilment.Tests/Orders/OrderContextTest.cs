using System;
using System.Threading.Tasks;
using EpicSoftware.Fulfilment.Context;
using EpicSoftware.Fulfilment.Domain.Order;
using EpicSoftware.Fulfilment.Repository.Orders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EpicSoftware.Fulfilment.Tests.Orders
{
    public class OrderContextTest
    {
        private readonly OrdersService.OrdersService _service;
        private readonly IOrderRepository _repository;
        
        public OrderContextTest()
        {
            _repository = GetInMemoryOrderRepository();
            _service = new OrdersService.OrdersService(_repository);
            AddNewOrder();
        }
        
        [Fact]
        public async Task OrdersServiceTest()
        {
            var result = await _service.GetAllOpenOrders();
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task VerifyOrderDetails()
        {
            var order = await _repository.GetById(1);
            var total = await _repository.GetAllOpenOrders();
            Assert.Equal(1, total.Count);
            Assert.Equal("51dssdcsdcsdc", order.ProductId);
            Assert.Equal(false, order.OrderComplete);
            Assert.Equal(1, order.UserId);
            Assert.Equal(1, order.Quantity);
            Assert.Equal(1, order.WorkflowId);
        }
        
        private void AddNewOrder()
        {
            var order = new Order
            {
                Id = 1,
                ProductId = "51dssdcsdcsdc",
                ProductJson = "{name: 'Test Product', price: 1.99, currency: 'USD'}",
                OrderDate = DateTime.Now,
                OrderCompleteDate = DateTime.Now,
                OrderComplete = false,
                UserId = 1,
                Quantity = 1,
                WorkflowId = 1
            };

            _repository.Create(order);

        }

        private static IOrderRepository GetInMemoryOrderRepository()
        {
            var builder = new DbContextOptionsBuilder<FulfilmentContext>();
            builder.UseInMemoryDatabase("Test");
            var options = builder.Options;
            var context = new FulfilmentContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return new OrderRepository(context);
        }
    }
}