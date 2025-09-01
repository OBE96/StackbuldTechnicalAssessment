using System.Text.Json.Serialization;


namespace StackbuldTechnicalAssessment.Application.Features.Products.Dtos
{
    public class GetProductsQueryParameters
    {
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 1;
    }
}
