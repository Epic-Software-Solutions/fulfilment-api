using AutoMapper;
using EpicSoftware.Fulfilment.Domain.Order;
using EpicSoftware.Fulfilment.Dtos.Orders;

namespace EpicSoftware.Fulfilment.Api.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}