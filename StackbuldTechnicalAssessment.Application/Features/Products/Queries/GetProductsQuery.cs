using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Application.Shared.Dtos;

using System.Text.Json.Serialization;


namespace StackbuldTechnicalAssessment.Application.Features.Products.Queries
{
    public class GetProductsQuery : IRequest<PagedListDto<ProductDto>>
    {
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 1;


    }
}
