using System.Text.Json.Serialization;


namespace StackbuldTechnicalAssessment.Application.Features.Orders.Dtos
{
    public class OrderCreateDto
    {
        [JsonPropertyName("pickup_name")]
        public string PickupName { get; set; }

        [JsonPropertyName("pickup_phonenumber")]
        public string PickupPhoneNumber { get; set; }

        [JsonPropertyName("pickup_email")]
        public string PickupEmail { get; set; }

        [JsonPropertyName("order_details")]
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }
}
