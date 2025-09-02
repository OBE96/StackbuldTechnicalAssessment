using StackbuldTechnicalAssessment.Application.Features.Orders.Dtos;
using StackbuldTechnicalAssessment.Domain.Entities;

namespace StackbuldTechnicalAssessment.Application.Features.Orders.Mappers
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
            CreateMap<OrderDetailDto, OrderDetails>().ReverseMap();
            CreateMap<OrderCreateDto, List<OrderDetails>>()
                .ConvertUsing(src => src.OrderDetails
                    .Select(d => new OrderDetails
                    {
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList());
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.OrderDetails,
                           opt => opt.MapFrom(src => src.OrderDetails));
        }
    }
}
