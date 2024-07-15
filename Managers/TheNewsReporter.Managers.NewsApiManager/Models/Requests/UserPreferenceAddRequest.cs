using System.Text.Json.Serialization;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;

namespace TheNewsReporter.Managers.NewsApiManager.Models.Requests
{
    public record UserPreferenceAddRequest
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("prefered_categories")]
        public List<string>? Categories { get; set; }

        [JsonPropertyName("news_preferences")]
        public List<string>? NewsPreferences { get; set; }

        [JsonPropertyName("communication_channel")]
        public CommunicationChannel? CommunicationChannel { get; set; }
    }
}
