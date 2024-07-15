using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.UserPreferencesService.Models
{
    public record UserPreferenceUpdateRequest
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
