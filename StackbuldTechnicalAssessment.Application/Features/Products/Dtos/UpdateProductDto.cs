using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StackbuldTechnicalAssessment.Application.Features.Products.Dtos
{
    public class UpdateProductDto
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("stockquantity")]
        public int StockQuantity { get; set; }
    }
}
