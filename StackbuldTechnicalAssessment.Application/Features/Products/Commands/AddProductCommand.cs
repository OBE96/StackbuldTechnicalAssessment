using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;

namespace StackbuldTechnicalAssessment.Application.Features.Products.Commands
{
    public class AddProductsCommand : IRequest<ProductsDto>
    {
        public AddProductsCommand(List<ProductCreationDto> productCreation)
        {
            productBody = productCreation;
        }

        public List<ProductCreationDto> productBody { get; }
    }
}
