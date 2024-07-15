using System.Text.Json.Serialization;
using TheNewsReporter.Accessors.AIAssistentService.Models.Articles;

namespace TheNewsReporter.Accessors.AIAssistentService.Models.Requests
{
    public class AIAssistantRequest
    {
        [JsonPropertyName("user_preferences")]
        public Preferences UserPreferences { get; set; }

        [JsonPropertyName("articles")]
        public List<NewsArticle> Articles { get; set; }
    }
}
