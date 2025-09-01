using System.Text.Json.Serialization;


namespace StackbuldTechnicalAssessment.Application.Shared.Dtos
{
    public class SuccessResponseDto<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = "Request completed successfully.";
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; } = 200;
    }
}
