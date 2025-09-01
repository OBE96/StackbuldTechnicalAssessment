using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;

namespace StackbuldTechnicalAssessment.Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public Guid Id { get; }
        public UpdateProductDto UpdateProductDto { get; }
        public UpdateProductCommand(Guid id, UpdateProductDto updateProductDto)
        {
            Id = id;
            UpdateProductDto = updateProductDto;
        }
    }
}
