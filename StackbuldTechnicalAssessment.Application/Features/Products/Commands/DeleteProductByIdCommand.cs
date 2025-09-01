using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;


namespace StackbuldTechnicalAssessment.Application.Features.Products.Commands
{
    public class DeleteProductByIdCommand : IRequest<ProductDto>
    {
        public Guid ProductId { get; }
        public DeleteProductByIdCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}
