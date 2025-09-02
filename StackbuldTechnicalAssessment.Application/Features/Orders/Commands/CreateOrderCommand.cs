using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Orders.Dtos;


namespace StackbuldTechnicalAssessment.Application.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<OrderSuccessAndErrorResponseDto<OrderResponseDto>>
    {
        public OrderCreateDto OrderCreateDto { get; set; }
        public CreateOrderCommand(OrderCreateDto orderCreateDto)
        {
            OrderCreateDto = orderCreateDto;
        }
    }
}
