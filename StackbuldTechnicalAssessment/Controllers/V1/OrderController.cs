using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackbuldTechnicalAssessment.Application.Features.Orders.Commands;
using StackbuldTechnicalAssessment.Application.Features.Orders.Dtos;
using StackbuldTechnicalAssessment.Application.Shared.Dtos;

namespace StackbuldTechnicalAssessment.Web.Controllers.V1
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Add Order - add a order 
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderSuccessAndErrorResponseDto<OrderResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(FailureResponseDto<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] OrderCreateDto body)
        {
            var result = await _mediator.Send(new CreateOrderCommand(body));

            return StatusCode(result.StatusCode, result);
        }
    }
}
