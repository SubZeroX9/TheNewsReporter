using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.NotificationApiService.Models
{
    public class NotificationRequest
    {
        [JsonPropertyName("channel")]
        public CommunicationChannel? Channel { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
