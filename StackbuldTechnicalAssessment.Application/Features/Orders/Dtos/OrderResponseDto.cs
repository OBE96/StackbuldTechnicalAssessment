using System.Text.Json.Serialization;

namespace StackbuldTechnicalAssessment.Application.Features.Orders.Dtos
{
    public class OrderResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("pickup_name")]
        public string PickupName { get; set; }

        [JsonPropertyName("pickup_phonenumber")]
        public string PickupPhoneNumber { get; set; }

        [JsonPropertyName("pickup_email")]
        public string PickupEmail { get; set; }

        [JsonPropertyName("order_total")]
        public double OrderTotal { get; set; }

        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }

        [JsonPropertyName("order_date")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("order_details")]
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }
}
