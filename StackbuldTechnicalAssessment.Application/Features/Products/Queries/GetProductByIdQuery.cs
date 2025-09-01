using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;


namespace StackbuldTechnicalAssessment.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
