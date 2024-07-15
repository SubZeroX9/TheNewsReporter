using System.Text.Json.Serialization;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;

namespace TheNewsReporter.Managers.NewsApiManager.Models.Requests
{
    public class NotificationRequest
    {
        [JsonPropertyName("channel")]
        public CommunicationChannel? Channel { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
