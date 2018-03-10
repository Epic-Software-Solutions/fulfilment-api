using System;
using System.Threading.Tasks;
using AutoMapper;
using EpicSoftware.Fulfilment.Context;
using EpicSoftware.Fulfilment.Domain.Order;
using EpicSoftware.Fulfilment.Dtos.Orders;
using EpicSoftware.Fulfilment.Repository.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EpicSoftware.Fulfilment.Tests.Orders
{
    public class OrderContextTest
    {
        private readonly OrdersService.OrdersService _service;
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        
        public OrderContextTest()
        {
            _repository = GetInMemoryOrderRepository();
            var mapperConfiguration = CreateConfiguration();
            var mapper = new Mapper(mapperConfiguration);
            
            _service = new OrdersService.OrdersService(_repository, mapper);
            AddNewOrder();
        }
        
        [Fact]
        public async Task GetAllOrdersServiceTest()
        {
            var result = await _service.GetAllOpenOrders();
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task CreateNewOrderViaService()
        {
            var order = new OrderDto()
            {
                Id = 2,
                ProductId = "csklcdmsdklcmkldscm",
                ProductJson = "{name: 'Test Product 1', price: 2.99, currency: 'GBP'}",
                OrderDate = DateTime.Now,
                OrderCompleteDate = DateTime.Now,
                OrderComplete = false,
                UserId = 1,
                Quantity = 1,
                WorkflowId = 1
            };
            
            await _service.CreateNewOrder(order);
            var result = await _service.GetAllOpenOrders();
            var insertedOrder = await _repository.GetById(2);
            Assert.Equal(2, result.Count);
            Assert.Equal("csklcdmsdklcmkldscm", insertedOrder.ProductId);
            Assert.Equal("{name: 'Test Product 1', price: 2.99, currency: 'GBP'}", insertedOrder.ProductJson);
            Assert.Equal(1, insertedOrder.UserId);
            Assert.Equal(1, insertedOrder.Quantity);
            Assert.Equal(1, insertedOrder.WorkflowId);
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

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderDto, Order>();
            });

            return config;
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