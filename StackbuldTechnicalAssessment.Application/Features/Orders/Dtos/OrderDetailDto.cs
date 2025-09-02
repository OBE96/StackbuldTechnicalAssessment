using System.Text.Json.Serialization;

namespace StackbuldTechnicalAssessment.Application.Features.Orders.Dtos
{
    public class OrderDetailDto
    {

        [JsonPropertyName("product_id")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unitprice")]
        public decimal UnitPrice { get; set; }
    }
}
